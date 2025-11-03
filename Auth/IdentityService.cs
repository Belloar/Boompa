using BCrypt.Net;
using Boompa.DTO;
using Boompa.Entities.Identity;
using Boompa.Exceptions;
using Boompa.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Boompa.Auth
{
    public class IdentityService : IIdentityService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        private readonly IConfiguration _configuration;
        public IdentityService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            
            _configuration = configuration;
        }
       

        public async Task UpdateAsync(string email, IdentityDTO.UpdateRequestModel model)
        {
            var user = new User();
            var credentials = await _unitOfWork.Identity.GetUserAsync(email);
            if (credentials == null)
            {
                throw new ServiceException("user not found");

            }
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.LastModifiedBy = credentials.UserName;
            user.LastModifiedOn = DateTime.UtcNow;

            await _unitOfWork.Identity.UpdateAsync(credentials.Id, user);
            

        }
       
        public async Task<Response> GetUsersAsync()
        {
            try
            {
                var response = new Response();
                var result = await _unitOfWork.Identity.GetUsersAsync();

                response.StatusMessages.Add("Success");
                response.Data = result;
                return response;
            }
            catch (Exception ex)
            {

                throw new ServiceException($"{ex.Message},{ex.InnerException?.Message}");
            }

        }
        public Task<User> GetUserAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<User> GetUserAsync(string searchString)
        {
            var validUser = await _unitOfWork.Identity.GetUserAsync(searchString);
            return validUser;
        }
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task  UpdateAsync(int id, IdentityDTO.UpdateRequestModel model)
        {
            throw new NotImplementedException();
        }
        
        
        public Task<string> GenerateToken(IdentityDTO.ValidUser validUser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            IList<Claim> claims = new List<Claim>()
            {
                new Claim ("userId",validUser.UserId.ToString()),
                new Claim(ClaimTypes.NameIdentifier,validUser.UserName),
                new Claim(ClaimTypes.Email ,validUser.Email),
                
                    
            };
            foreach (var userRole in validUser.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials:credentials);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));

        }
        public async Task<IdentityDTO.ValidUser> AuthenticateUser(string searchString, string password)
        {
            var isEmail = false;
            var user = new User();
            if (searchString.Contains('@')) isEmail = true;

            if (isEmail) await _unitOfWork.Identity.GetUserAsync(searchString,true);
            user = await _unitOfWork.Identity.GetUserAsync(searchString);
            if (user == null) throw new IdentityException("User doesn't exist");
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password)) throw new ServiceException("Invalid Username or Password");


            var model = new IdentityDTO.ValidUser
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
            };
            foreach (var userRole in user.Roles)
            {
                model.Roles.Add(userRole.Role.RoleName);
            }
            return model;
        }
        public Task AddRoleAsync(string role)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserRole(int id,string role)
        {
            var newRole = await _unitOfWork.Identity.GetRoleAsync(role);
            if (newRole == null) throw new NotFoundException("this role does not exist or is already deleted");
             await _unitOfWork.Identity.UpdateUserRole(id,newRole);
            
        }
        private static IEnumerable<Claim> GetClaims(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            var claims = jwtSecurityToken.Claims;
            return claims;
        }

        public async Task<bool> CheckUser(string email)
        {
            var exists = await _unitOfWork.Identity.CheckUser(email);
            if (exists) throw new IdentityException("this user already exists");
            return true;
        }
    }
}

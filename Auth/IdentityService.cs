using BCrypt.Net;
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
        private readonly IIdentityRepository _repository;
        private readonly IConfiguration _configuration;
        public IdentityService(IIdentityRepository repository,IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
       

        public async Task<int> UpdateAsync(string email, IdentityDTO.UpdateRequestModel model, CancellationToken cancellationToken)
        {
            var user = new User();
            var credentials = await _repository.GetUserAsync(email);
            if (credentials == null)
            {
                return 0;

            }
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.LastModifiedBy = credentials.UserName;
            user.LastModifiedOn = DateTime.UtcNow;

            var result = await _repository.UpdateAsync(credentials.Id, user, cancellationToken);
            return result;

        }
       
        public Task<IEnumerable<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }
        public Task<User> GetUserAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<User> GetUserAsync(string searchString)
        {
            var validUser = await _repository.GetUserAsync(searchString);
            return validUser;
        }
        public Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task<int> UpdateAsync(int id, IdentityDTO.UpdateRequestModel model, CancellationToken cancellationToken)
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
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials:credentials);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));

        }
        public async Task<IdentityDTO.ValidUser> AuthenticateUser(string searchString, string password)
        {
            var isEmail = false;
            var user = new User();
            if (searchString.Contains('@')) isEmail = true;

            if (isEmail) _ = await _repository.GetUserAsync(searchString,true);
            user = await _repository.GetUserAsync(searchString);
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
        public Task<int> AddRoleAsync(string role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateUserRole(int id,string role, CancellationToken cancellationToken)
        {
            var newRole = await _repository.GetRoleAsync(role);
            if (newRole == null) throw new NotFoundException("this role does not exist or is already deleted");
            var result = await _repository.UpdateUserRole(id,newRole,cancellationToken);
            return result;
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
            var exists = await _repository.CheckUser(email);
            if (exists) throw new IdentityException("this user already exists");
            return true;
        }
    }
}

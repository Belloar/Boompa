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
        public Task<int> CreateAsync(IdentityDTO.CreateRequestModel model, CancellationToken cancellationToken)
        {
            var role = _repository.GetRoleAsync("user");
            CheckEmail(model.Email);
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                CreatedBy = model.UserName,
                Hashsalt = BCrypt.Net.BCrypt.GenerateSalt()
                
            };
            if (CheckEmail(model.Email)) user.IsEmailConfirmed = true; else user.IsEmailConfirmed = false;
            user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password,user.Hashsalt);
            user.UserRoles = new HashSet<UserRole>
                {
                    new UserRole
                    {
                        RoleId = role.Id,
                        UserId = user.Id
                    }
                };
            return _repository.CreateAsync(user, cancellationToken);
        }

        public async Task<int> UpdateAsync(string email, IdentityDTO.UpdateRequestModel model, CancellationToken cancellationToken)
        {
            var user = new User();
            var credentials = await AuthenticateUser(email);
            if (credentials == null)
            {
                return 0;

            }
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.LastModifiedBy = credentials.UserName;
            user.LastModifiedOn = DateTime.UtcNow;

            var result = await _repository.UpdateAsync(credentials.UserId, user, cancellationToken);
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
        public Task<User> GetUserAsync(string checkString)
        {
            throw new NotImplementedException();
        }
        public Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task<int> UpdateAsync(int id, IdentityDTO.UpdateRequestModel model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async Task<IdentityDTO.ValidUserModel> AuthenticateUser(string email)
        {
            var user = await _repository.GetUserAsync(email);
            if (user == null) return null;
            var model = new IdentityDTO.ValidUserModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = user.UserRoles 
            };
            return model;
        }
        public bool CheckEmail(string email)
        {
            var result = email.Contains('@');
            return result;
        }
        public Task<string> GenerateToken(User validUser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            IList<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,validUser.UserName),
                new Claim(ClaimTypes.Email,validUser.Email),
                    
            };
            foreach (var userRole in validUser.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.RoleName));
            }
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials:credentials);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));

        }
        public async Task<User> AuthenticateUser(string username, string password)
        {
            var user = await _repository.GetUserAsync(username, password);
            if(user == null)
            {
                return null;
            }
            return user;
        }
        public Task<int> AddRoleAsync(string role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateUserRole(string role, CancellationToken cancellationToken)
        {
            var newRole = await _repository.GetRoleAsync(role);
            if (newRole == null) throw new NotFoundException("this role does not exist or is already deleted");
            var result = await _repository.UpdateUserRole(newRole,cancellationToken);
            return result;
        }
    }
}

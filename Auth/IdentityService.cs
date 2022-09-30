using Boompa.Entities.Identity;
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
            CheckEmail(model.Email);
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                CreatedBy = model.UserName,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                DeletedBy = null,
                DeletedOn = null,
                LastModifiedBy = null,
                LastModifiedOn = null,
            };
            if (CheckEmail(model.Email)) user.IsEmailConfirmed = true; else user.IsEmailConfirmed = false;
            user.Password = model.Password;//Hashsalt
            //Hashpassword here before assigning it
            return _repository.CreateAsync(user, cancellationToken);
        }

        public async Task<int> UpdateAsync(string email, IdentityDTO.UpdateRequestModel model, CancellationToken cancellationToken)
        {
            var user = new User();
            var credentials = await ValidateUser(email);
            if (credentials == null)
            {
                return 0;

            }
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.LastModifiedBy = credentials.Username;
            user.LastModifiedOn = DateTime.UtcNow;

            var result = await _repository.UpdateAsync(credentials.Userid, user, cancellationToken);
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

        public async Task<IdentityDTO.ValidatedUserModel> ValidateUser(string email)
        {
            var user = await _repository.GetUserAsync(email);
            if (user == null)
            {
                return null;

            }
            var model = new IdentityDTO.ValidatedUserModel
            {
                Userid = user.Id,
                Username = user.UserName,
                Email = user.Email,
            };
            return model;
        }
        public bool CheckEmail(string email)
        {
            var result = email.Contains('@');
            return result;
        }

        public string GenerateToken(IdentityDTO.ValidatedUserModel validUser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            IList<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,validUser.Username),
                new Claim(ClaimTypes.Email,validUser.Email),
                    
            };
            foreach (var role in validUser.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Role.RoleName));
            }
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddMinutes(10), signingCredentials:credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public User AuthenticateUser(string username, string password)
        {
            var user =  _repository.GetUserAsync(username, password);
            if(user == null)
            {
                return null;
            }
            return user;
        }
    }
}

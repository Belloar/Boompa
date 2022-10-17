using Boompa.Auth;
using Boompa.DTO;
using Boompa.Entities;
using Boompa.Entities.Identity;
using Boompa.Exceptions;
using Boompa.Interfaces.IRepository;
using Boompa.Interfaces.IService;

namespace Boompa.Implementations.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepo;
        private readonly IIdentityRepository _identityRepo;
            public AdminService(IAdminRepository repository,IIdentityRepository identityrepo)
        {
            _adminRepo = repository;
            _identityRepo = identityrepo;
        }
        public async Task<int> CreateAdminAsync(AdminDTO.CreateModel model, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (_identityRepo.CheckUser(model.Email)) throw new IdentityException("a user with this email already exists");

            var role1 = await _identityRepo.GetRoleAsync("user");
            var role2 = await _identityRepo.GetRoleAsync("Admin");
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = "0000",
                CreatedBy = model.UserName,
                Hashsalt = BCrypt.Net.BCrypt.GenerateSalt(),
                IsEmailConfirmed = true,


            };
            user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password, user.Hashsalt);
            await _identityRepo.CreateAsync(user, cancellationToken);
            user.Roles = new HashSet<UserRole>()
            {
                new UserRole
                {
                    User = user,
                    RoleId = role1.Id,
                    Role = role1,
                    UserId = user.Id
                },
                new UserRole
                {
                    Role = role2,
                    RoleId = role2.Id,
                    User = user,
                    UserId = user.Id
                }
            };
            await _identityRepo.AddUserRole(user.Roles, cancellationToken);
            if (user == null) throw new IdentityException("this user does not exist");


            var admin = new Administrator()
            {
                User = user,
                UserId = user.Id,
                CreatedBy = model.UserName,
                PhoneNumber = "0000"
            };
            var result = await _adminRepo.AddAdminAsync(admin, cancellationToken);
            if (result == 0) throw new ServiceException("failed to create profile");
            return result;
        }

        public Task<int> CreateArticleAsync(MaterialDTO.ArticleModel article, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateCategoryAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateChallengeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateOptionAsync(MaterialDTO.OptionModel option, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateQuestionAsync(MaterialDTO.QuestionModel question, MaterialDTO.OptionModel option, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateRoleAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAdminAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteArticleAsync(string articleName)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteCategoryAsync(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteQuestionAsync(string articleName, int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Administrator> GetAdminAsync(string adminName)
        {
            throw new NotImplementedException();
        }

        public Task<Administrator> GetAdminAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Administrator>> GetAdminsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Learner> GetLearnerAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Learner>> GetLearnersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAdminAsync(AdminDTO.CreateModel requestModel, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateQuestion()
        {
            throw new NotImplementedException();
        }

        public Task<int> UploadArticleAsync(IFormFile file, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> UploadImageAsync(IFormFile file, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

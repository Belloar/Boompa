using Boompa.Auth;
using Boompa.DTO;
using Boompa.Entities;
using Boompa.Entities.Identity;
using Boompa.Exceptions;
using Boompa.Interfaces;
using Boompa.Interfaces.IRepository;
using Boompa.Interfaces.IService;

namespace Boompa.Implementations.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISourceMaterialService _sourceMaterialService;
        
        public AdminService(IUnitOfWork unitOfWork, ISourceMaterialService sourceMaterialService)
        {
            _unitOfWork = unitOfWork;
           // _sourceMaterialService = sourceMaterialService;
            
        }

        public Task<int> AddSourceMaterial(MaterialDTO.ArticleModel article)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateAdminAsync(AdminDTO.CreateModel model)
        {
            
            if (await _unitOfWork.Identity.CheckUser(model.Email)) throw new IdentityException("a user with this email already exists");

            var role1 = await _unitOfWork.Identity.GetRoleAsync("user");
            var role2 = await _unitOfWork.Identity.GetRoleAsync("Admin");
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                CreatedBy = model.UserName,
                HashSalt = BCrypt.Net.BCrypt.GenerateSalt(),
                IsEmailConfirmed = true,


            };
            user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password, user.HashSalt);
            user.Roles = new HashSet<UserRole>
            {
                new UserRole
                {
                    User = user,
                    RoleId = role1.Id,
                },
                new UserRole
                {
                    User = user,
                    RoleId = role2.Id,
                }
            };
            await _unitOfWork.Identity.CreateAsync(user);

            if (user == null) throw new IdentityException("this user does not exist");


            var admin = new Admin()
            {
                
                CreatedBy = model.UserName,
                PhoneNumber = "0000"
            };
            _unitOfWork.Admins.AddAdminAsync(admin);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result < 1) throw new ServiceException("failed to create profile");
            return result;
        }

       

        public Task<int> CreateCategoryAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateChallengeAsync()
        {
            throw new NotImplementedException();
        }
        public Task<int> CreateQuestionAsync(string question)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateRoleAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAdminAsync(int id)
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

        public Task<int> DeleteQuestionAsync(string articleName, int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteRoleAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public async Task<Admin> GetAdminAsync(string adminName)
        {
            var admin = await _unitOfWork.Admins.GetAdminAsync(adminName);
            if (admin == null) throw new ServiceException("admin does not exist");
            return admin;
        }

        public async Task<Admin> GetAdminAsync(int id)
        {
            var admin = await _unitOfWork.Admins.GetAdminAsync(id);
            if (admin == null) throw new ServiceException("admin does not exist");
            return admin;
        }

        public async Task<IEnumerable<Admin>> GetAdminsAsync()
        {
            var admins = await _unitOfWork.Admins.GetAdminsAsync();
            if (admins == null) throw new ServiceException("An error occured while retrieving admins");
            return admins;
        }

        //public async Task<Learner> GetLearnerAsync(string name)
        //{
        //    var learner = await _unitOfWork.Learners.GetLearner(name);
        //    if (learner == null) throw new IdentityException("This learner does not exist");
        //    return learner;
        //}

        public Task<IEnumerable<Learner>> GetLearnersAsync()
        {
            var result = _unitOfWork.Learners.GetLearners(false);
            if (result == null) throw new ServiceException("An error was encountered during the process");
            return result;
        }

        public Task<int> UpdateAdminAsync(AdminDTO.UpdateModel model)
        {
            
            throw new NotImplementedException();
        }

        public Task<int> UpdateQuestion()
        {
            throw new NotImplementedException();
        }

        public Task<int> UploadArticleAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task<int> UploadImageAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}

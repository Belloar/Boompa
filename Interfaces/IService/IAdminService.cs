using Boompa.DTO;
using Boompa.Entities;
using Boompa.Entities.Identity;

namespace Boompa.Interfaces.IService
{
    public interface IAdminService
    {
        Task<int> AddSourceMaterial(MaterialDTO.ArticleModel article);
        Task<int> CreateAdminAsync(AdminDTO.CreateModel model);
        Task<int> CreateChallengeAsync();
        Task<int> CreateCategoryAsync();
        Task<int> CreateRoleAsync(Role role);
        Task<int> DeleteAdminAsync(int id);
        Task<int> DeleteQuestionAsync(string articleName,int id);
        Task<int> DeleteCategoryAsync(string categoryName);
        Task<int> DeleteRoleAsync(string roleName);
        Task<IEnumerable<Admin>> GetAdminsAsync();
        Task<Admin> GetAdminAsync(int id);
        Task<Admin> GetAdminAsync(string checkString);
        Task<IEnumerable<Learner>> GetLearnersAsync();
        Task<Learner> GetLearnerAsync(string name);
        Task<int> UpdateAdminAsync(AdminDTO.UpdateModel model);
        Task<int> UpdateQuestion();
        
    }
}

using Boompa.Entities;

namespace Boompa.Interfaces.IService
{
    public interface IAdminService
    {
        Task<int> CreateQuestionAsync();
        Task<int> CreateArticleAsync();
        Task<int> CreateOptionAsync();
        Task<int> CreateChallengeAsync();
        Task<int> CreateCategoryAsync();
        Task<int> CreateRoleAsync();
        Task<int> DeleteQuestionAsync();
        Task<int> DeleteArticleAsync(string articleName);
        Task<int> DeleteCategoryAsync(string categoryName);
        Task<int> DeleteRoleAsync(string roleName);
        Task<Administrator> GetAdminAsync(string adminName);
        Task<IEnumerable<Learner>> GetLearners();
        Task<Learner> GetLearnerAsync(string name);
        Task<int> UpdateQuestion();
        Task<int> UploadArticleAsync(IFormFile file);


    }
}

using Boompa.DTO;
using Boompa.Entities;
using Boompa.Entities.Identity;

namespace Boompa.Interfaces.IService
{
    public interface IAdminService
    {
        Task<int> CreateQuestionAsync(MaterialDTO.QuestionModel question,MaterialDTO.OptionModel option,CancellationToken cancellationToken);
        Task<int> CreateAdminAsync(AdminDTO.CreateModel model, CancellationToken cancellationToken);
        Task<int> CreateArticleAsync(MaterialDTO.ArticleModel article,CancellationToken cancellationToken);
        Task<int> CreateOptionAsync(MaterialDTO.OptionModel option, CancellationToken cancellationToken);
        Task<int> CreateChallengeAsync();
        Task<int> CreateCategoryAsync();
        Task<int> CreateRoleAsync(Role role);
        Task<int> DeleteAdminAsync(int id, CancellationToken cancellationToken);
        Task<int> DeleteQuestionAsync(string articleName,int id,CancellationToken cancellationToken);
        Task<int> DeleteArticleAsync(string articleName);
        Task<int> DeleteCategoryAsync(string categoryName);
        Task<int> DeleteRoleAsync(string roleName,CancellationToken cancellationToken);
        Task<IEnumerable<Administrator>> GetAdminsAsync();
        Task<Administrator> GetAdminAsync(int id);
        Task<Administrator> GetAdminAsync(string checkString);
        Task<IEnumerable<Learner>> GetLearnersAsync();
        Task<Learner> GetLearnerAsync(string name);
        Task<int> UpdateAdminAsync(AdminDTO.UpdateModel model, CancellationToken cancellationToken);
        Task<int> UpdateQuestion();
        Task<int> UploadArticleAsync(IFormFile file,CancellationToken cancellationToken);
        Task<int> UploadImageAsync(IFormFile file, CancellationToken cancellationToken);


    }
}

using Boompa.DTO;
using Boompa.Entities;
using Boompa.Enums;

namespace Boompa.Interfaces.IRepository
{
    public interface ISourceMaterialRepository
    {
        
        Task<int> AddSourceMaterial(SourceMaterial sourceMaterial);
        Task<int> AddChallengeAsync();
        Task<int> AddFileDeets(List<SourceFileDetail> files);
        Task<Question> AddQuestionAsync(Question model);
        Task<int> AddOptionAsync(IEnumerable<Option> options);
        Task<int> DeleteSourceMaterial();
        Task<int> DeleteQuestionAsync();
        Task<SourceMaterial> GetById(int id);
        //Task<Category> GetCategoryByNameAsync(string categoryName);
        Task<int> UpdateQuestion();
        
    }
}

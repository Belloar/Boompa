using Boompa.DTO;
using Boompa.Entities;


namespace Boompa.Interfaces.IRepository
{
    public interface ISourceMaterialRepository
    {
        Task AddCategory(Category category);
        Task<SourceMaterial> AddSourceMaterial(SourceMaterial sourceMaterial);
        Task AddChallengeAsync();
        Task<Question> AddQuestionAsync(Question model);
        //Task<Question> AddQuestionAsync(Question model,string sourceName, string category);

        Task<bool> CategoryExists(string categoryName);
        Task<Category> GetCategoryId(string categoryName);
        Task<ICollection<MaterialDTO.SourceDescriptor>> GetAll();
        Task<ICollection<CategorySourceMaterial>> GetByCategory(Guid Id);
        Task DeleteSourceMaterial();
        Task DeleteQuestionAsync();
        Task<SourceMaterial> GetById(int id);
        Task<SourceMaterial> GetSourceMaterial(string sourceMaterialName, string category);
        Task<SourceMaterial> GetSourceMaterial(Guid sourceId);
        Task UpdateQuestion();
        
    }
}

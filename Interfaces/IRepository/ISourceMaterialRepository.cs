using Boompa.DTO;
using Boompa.Entities;


namespace Boompa.Interfaces.IRepository
{
    public interface ISourceMaterialRepository
    {
        Task AddCategory(Category category);
        Task<SourceMaterial> AddSourceMaterial(SourceMaterial sourceMaterial);
        Task<Question> AddQuestionAsync(Question model);
        //Task<Question> AddQuestionAsync(Question model,string sourceName, string category);

        Task<bool> CategoryExists(string categoryName);
        Task<Category> GetCategoryId(string categoryName);
        Task<Category> GetCategory(Guid Id);
        Task<ICollection<MaterialDTO.SourceDescriptor>> GetAll(int skipCount);
        Task<ICollection<MaterialDTO.SourceDescriptor>> GetAll(Guid categoryId, int skipCount);
        Task<ICollection<CategorySourceMaterial>> GetByCategory(Guid Id);
        Task DeleteSourceMaterial();
        Task DeleteQuestionAsync();
        Task<SourceMaterial> GetById(int id);
        Task<SourceMaterial> GetSourceMaterial(string sourceMaterialName, string category);
        Task<SourceMaterial> GetSourceMaterial(Guid sourceId);
        Task UpdateQuestion();
        Task<SourceMaterial> GetRandomSource();
        Task<ICollection<MaterialDTO.CategoryDetails>> GetTopCategories(string learnerId);
        Task<ICollection<MaterialDTO.CategoryDetails>> GetCategories();

    }
}

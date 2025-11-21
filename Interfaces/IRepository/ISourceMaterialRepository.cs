using Boompa.DTO;
using Boompa.Entities;


namespace Boompa.Interfaces.IRepository
{
    public interface ISourceMaterialRepository
    {
        
        Task<SourceMaterial> AddSourceMaterial(SourceMaterial sourceMaterial);
        Task AddChallengeAsync();
        Task<Question> AddQuestionAsync(Question model,string sourceName, string category);
        Task<bool> CategoryExists(string categoryName);
        Task<Category> GetCategory(string categoryName);
        Task<ICollection<MaterialDTO.SourceDescriptor>> GetAllSourceMaterials(string categoryName);
        Task DeleteSourceMaterial();
        Task DeleteQuestionAsync();
        Task<SourceMaterial> GetById(int id);
        Task<SourceMaterial> GetSourceMaterial(string sourceMaterialName, string category);
        Task UpdateQuestion();
        
    }
}

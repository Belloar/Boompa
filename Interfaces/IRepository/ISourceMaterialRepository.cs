using Boompa.DTO;
using Boompa.Entities;
using Boompa.Enums;

namespace Boompa.Interfaces.IRepository
{
    public interface ISourceMaterialRepository
    {
        
        Task AddSourceMaterial(SourceMaterial sourceMaterial);
        Task AddChallengeAsync();
        Task AddCloudSourceFile(CloudSourceFileDetails cloudSourceFile);
        Task AddCloudEvalFile(CloudEvalFileDetails cloudEvalFile);
        Task AddFileDetail(List<SourceFileDetail> files);
        Task AddFileDetail(List<QuestionFileDetail> files);
        Task AddQuestionAsync(Question model);
        Task<ICollection<MaterialDTO.SourceDescriptor>> GetAllSourceMaterials(string categoryName);
        Task DeleteSourceMaterial();
        Task DeleteQuestionAsync();
        Task<SourceMaterial> GetById(int id);
        Task<SourceMaterial> GetSourceMaterial(string sourceMaterialName, string category);
        Task UpdateQuestion();
        
    }
}

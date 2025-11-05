using Boompa.DTO;
using Boompa.Entities;
using Boompa.Enums;

namespace Boompa.Interfaces.IRepository
{
    public interface ISourceMaterialRepository
    {
        
        Task<SourceMaterial> AddSourceMaterial(SourceMaterial sourceMaterial);
        Task AddChallengeAsync();
        Task AddCloudSourceFile(CloudSourceFileDetails cloudSourceFile);
        Task AddCloudEvalFile(CloudEvalFileDetails cloudEvalFile);
        //Task AddFileDetail(List<SourceFileDetail> files);
        //Task AddFileDetail(List<QuestionFileDetail> files);
        Task<Question> AddQuestionAsync(Question model);
        Task<ICollection<MaterialDTO.SourceDescriptor>> GetAllSourceMaterials(string categoryName);
        Task DeleteSourceMaterial();
        Task DeleteQuestionAsync();
        Task<SourceMaterial> GetById(int id);
        Task<SourceMaterial> GetSourceMaterial(string sourceMaterialName, string category);
        Task UpdateQuestion();
        
    }
}

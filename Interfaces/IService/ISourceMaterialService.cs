using Boompa.DTO;
using Boompa.Entities;

using Boompa.DTO;
namespace Boompa.Interfaces.IService
{
    public interface ISourceMaterialService
    {
        
        Task<Response> AddCategory(string category);
        Task<Response> AddQuestion(MaterialDTO.QuestionModel model,int sourceMaterialId);
        Task<Response> AddSourceMaterial(MaterialDTO.ArticleModel material);
        Task<Response> DeleteSourceMaterial();
        Task<Response> GetAllSourceMaterials();//ICollection<MaterialDTO.SourceDescriptor>
        Task<Response> GetSourceMaterial(string sourceMaterialName, string category);
        Task<Response> UpdateQuestion(string model);
        Task<Response> UpdateSourceMaterial(MaterialDTO rawMaterial);
        
    }
}

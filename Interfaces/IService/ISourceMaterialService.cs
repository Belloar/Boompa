using Boompa.DTO;
using Boompa.Entities;
namespace Boompa.Interfaces.IService
{
    public interface ISourceMaterialService
    {
        
        Task<Response> AddCategory(string category);
        Task<Response> AddQuestion(MaterialDTO.QuestionModel model,int sourceMaterialId);
        Task<Response> AddQuestion(IEnumerable<MaterialDTO.QuestionModel> models);
        Task<Response> AddSourceMaterial(MaterialDTO.ArticleModel material);
        Task<Response> DeleteSourceMaterial();
        Task<Response> GetAllSourceMaterials(string categoryName);
        Task<Response> GetSourceMaterial(string sourceMaterialName, string category);
        Task<Response> UpdateQuestion(string model);
        Task<Response> UpdateSourceMaterial(MaterialDTO rawMaterial);
        
    }
}

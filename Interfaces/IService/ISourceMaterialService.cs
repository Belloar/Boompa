using Boompa.DTO;
using Boompa.Entities;
namespace Boompa.Interfaces.IService
{
    public interface ISourceMaterialService
    {
        
        Task<Response> AddCategory(string category);
        Task<Response> AddQuestion(MaterialDTO.QuestionModel model,Guid sourceMaterialId);
        Task<Response> AddQuestion(ICollection<MaterialDTO.QuestionModel> models,string sourceMaterialName, string category);
        Task<Response> AddQuestion(ICollection<MaterialDTO.QuestionModel> models,Guid sourceMaterialId);
        Task<Response> AddSourceMaterial(MaterialDTO.ArticleModel material,string creator = "administrator");
        Task<Response> AddSourceMaterial(MaterialDTO.TinyModel model);
        Task<Response> DeleteSourceMaterial();
        Task<Response> GetAllSourceMaterials(int pageNumber);
        Task<Response> GetAllSourceMaterials(Guid categoryId , int pageNumber);
        Task<Response> GetCategories();
        Task<Response> GetRandomSource();
        Task<Response> GetSourceMaterial(string sourceMaterialName, string category);
        Task<Response> GetSourceMaterial(Guid sourceId);
        Task<Response> GetTopCategories(string learnerId);
        Task<Response> UpdateQuestion(string model);
        Task<Response> UpdateSourceMaterial(MaterialDTO rawMaterial);
        
    }
}

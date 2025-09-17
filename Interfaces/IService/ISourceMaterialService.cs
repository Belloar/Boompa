using Boompa.DTO;
using Boompa.Entities;

namespace Boompa.Interfaces.IService
{
    public interface ISourceMaterialService
    {
        
        Task<int> AddCategory(string category);
        Task<int> AddQuestion(ICollection<MaterialDTO.QuestionModel> model,int sourceMaterialId);
        Task<int> AddSourceMaterial(MaterialDTO.ArticleModel material);//,ICollection<MaterialDTO.QuestionModel> queModel

        Task<int> DeleteSourceMaterial();
        Task<int>UpdateQuestion(string model);
        Task<int> UpdateSourceMaterial(MaterialDTO rawMaterial);
        
    }
}

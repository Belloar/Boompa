using Boompa.DTO;
using Boompa.Entities;

namespace Boompa.Interfaces.IService
{
    public interface ISourceMaterialService
    {
        Task<int> AddSourceMaterial(MaterialDTO.ArticleModel material);
        Task<int> AddCategory(string category);
        Task<int> DeleteSourceMaterial();
        Task<int>UpdateQuestion(string model);
        Task<int> UpdateSourceMaterial(MaterialDTO rawMaterial);
    }
}

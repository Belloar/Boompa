using Boompa.DTO;

namespace Boompa.Interfaces.IService
{
    public interface ISourceMaterialService
    {
        Task<int> AddSourceMaterial(MaterialDTO material);
        Task<int> DeleteSourceMaterial();
        Task<int>UpdateQuestion(MaterialDTO.QuestionModel model);
        Task<int> UpdateSourceMaterial(MaterialDTO rawMaterial);
    }
}

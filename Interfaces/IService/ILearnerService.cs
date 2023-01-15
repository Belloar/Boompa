using Boompa.DTO;
using Boompa.Entities;

namespace Boompa.Interfaces.IService
{
    public interface ILearnerService
    {
        Task<int> CreateLearner(LearnerDTO.CreateRequest model,CancellationToken cancellationToken);
        Task<IEnumerable<Article>> ConversationCompiler(string MaterialName);
        Task<int> DeleteLearner(int id,CancellationToken cancellationToken);
        
        Task<Article> GetMaterial(string MaterialName);
        Task<Learner> GetLearner(int id);
        Task<LearnerDTO.LearnerInfo> GetLearner(string checkString);
        Task<IEnumerable<LearnerDTO.LearnerInfo>> GetLearnersInfo();
        Task<IEnumerable<Learner>> GetLearners();
        Task<IEnumerable<Article>> LoadCategoryMaterials(string categoryName);
        Task<int> Play();
        Task<int> UpdateLearner(LearnerDTO.UpdateInfo model,CancellationToken cancellationToken);
        Task<int> UpdateLearner(LearnerDTO.UpdateStats model,CancellationToken cancellationToken);
        Task<Question> QuestionSelector();
        


    }
}

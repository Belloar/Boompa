using Boompa.DTO;
using Boompa.Entities;

namespace Boompa.Interfaces.IService
{
    public interface ILearnerService
    {
        Task<int> CreateLearner(LearnerDTO.CreateRequestModel model,CancellationToken cancellationToken);
        Task<IEnumerable<Article>> ConversationCompiler(string MaterialName);
        Task<int> DeleteLearner(int id,CancellationToken cancellationToken);
        Task<IEnumerable<Article>>LoadCategoryMaterials(string categoryName);
        Task<Article> GetMaterial(string MaterialName);
        Task<Learner> GetLearner(int id);
        Task<Learner> GetLearner(string checkString);
        Task<IEnumerable<Learner>> GetLearners();
        Task<Question> QuestionSelector();
        Task<int> UpdateLearner(int learnerId,LearnerDTO.UpdateRequestModel model,CancellationToken cancellationToken);
        Task<int> UpdateLearner(int Userid,LearnerDTO.UpdateStatsModel model,CancellationToken cancellationToken);


    }
}

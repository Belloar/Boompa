using Boompa.DTO;
using Boompa.Entities;

namespace Boompa.Interfaces.IService
{
    public interface ILearnerService
    {
        Task<int> CreateLearner(LearnerDTO.CreateLearner model);
        
        Task<int> DeleteLearner(Guid id);
        
        Task<SourceMaterial> GetMaterial(string MaterialName);
        Task<Learner> GetLearner(Guid id);
        Task<Response> GetLearner(string checkString);
        Task<IEnumerable<LearnerDTO.LearnerInfo>> GetLearnersInfo();
        Task<Response> GetLearnerInfo(string checkString);
        Task<Response> GetLearners(int numberOfRecordsToSkip);
        Task<int> UpdateLearner(LearnerDTO.UpdateInfo model,Guid LearnerId);
        Task<int> UpdateLearner(LearnerDTO.UpdateStats model,string userName);
        
        


    }
}

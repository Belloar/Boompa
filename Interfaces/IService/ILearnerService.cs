using Boompa.DTO;
using Boompa.Entities;

namespace Boompa.Interfaces.IService
{
    public interface ILearnerService
    {
        Task<int> CreateLearner(LearnerDTO.CreateRequest model);
        
        Task<int> DeleteLearner(int id);
        
        Task<SourceMaterial> GetMaterial(string MaterialName);
        Task<Learner> GetLearner(int id);
        Task<LearnerDTO.LearnerInfo> GetLearner(string checkString);
        Task<IEnumerable<LearnerDTO.LearnerInfo>> GetLearnersInfo();
        Task<IEnumerable<Learner>> GetLearners();
        Task<int> UpdateLearner(LearnerDTO.UpdateInfo model);
        Task<int> UpdateLearner(LearnerDTO.UpdateStats model);
        
        


    }
}

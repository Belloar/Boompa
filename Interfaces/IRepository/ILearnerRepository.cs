using Boompa.DTO;
using Boompa.Entities;
using Boompa.Entities.Identity;


namespace Boompa.Interfaces.IRepository
{
    public interface ILearnerRepository
    {
        Task AddLearner(Learner model);
        Task<int> DeleteLearner(LearnerDTO.DeleteModel model);
        Task<IEnumerable<Learner>> GetLearners(int numberOfRecordsToSkip = 0);
        Task<IEnumerable<Learner>> GetLearners( bool byStatus = false);
        Task<Learner> GetLearner(Guid id);
        Task<Learner> GetLearner(string searchString);

        Task UpdateLearner(Learner learner);
        
        



    }
}

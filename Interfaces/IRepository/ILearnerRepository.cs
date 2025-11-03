using Boompa.DTO;
using Boompa.Entities;
using Boompa.Entities.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Boompa.Interfaces.IRepository
{
    public interface ILearnerRepository
    {
        Task<int> AddLearner(Learner  model);
        Task<int> DeleteLearner(LearnerDTO.DeleteModel model);
        Task<IEnumerable<Learner>> GetLearners(bool byStatus = false);
        Task<Learner> GetLearner(int id);
        Task<Learner> GetLearner(string searchString);

        Task<int> UpdateLearner(Learner learner);
        



    }
}

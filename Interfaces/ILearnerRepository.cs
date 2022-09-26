using Boompa.DTO;
using Boompa.Entities;
using Boompa.Entities.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Boompa.Interfaces
{
    public interface ILearnerRepository
    {
        Task<bool> CreateLearnerAsync(LearnerDTO.CreateRequestModel requestModel, CancellationToken cancellationToken,User user);
        Task<bool> UpdateLearnerAsync(LearnerDTO.CreateRequestModel requestModel, CancellationToken cancellationToken);
        Task<IEnumerable<Learner>> GetLearnersAsync();
        Task<Learner> GetLearnerAsync(int id );
        Task<Learner> GetLearnerAsync(string checkString);
        

    }
}

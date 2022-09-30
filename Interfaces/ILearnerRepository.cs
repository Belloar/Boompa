using Boompa.DTO;
using Boompa.Entities;
using Boompa.Entities.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Boompa.Interfaces
{
    public interface ILearnerRepository
    {
        Task<int> CreateAsync(LearnerDTO.CreateRequestModel requestModel, CancellationToken cancellationToken,User user);
        Task<int> UpdateAsync(LearnerDTO.CreateRequestModel requestModel, CancellationToken cancellationToken);
        Task<IEnumerable<Learner>> GetLearnersAsync();
        Task<Learner> GetLearnerAsync(int id);
        Task<Learner> GetLearnerAsync(string checkString);
        Task<int> DeleteAsync(int id,CancellationToken cancellationToken);
        

    }
}

using Boompa.Context;
using Boompa.DTO;
using Boompa.Entities;
using Boompa.Entities.Identity;
using Boompa.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Boompa.Repositories
{
    public class LearnerRepository : ILearnerRepository
    {
        private readonly ApplicationContext _context;
        public LearnerRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateLearnerAsync(LearnerDTO.CreateRequestModel requestModel, CancellationToken cancellationToken,User user)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var learner = new Learner
            {
                UserId= user.Id,
                User = user,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                Age = requestModel.Age

            };
            await _context.Learners.AddAsync(learner,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public Task<Learner> GetLearnerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Learner> GetLearnerAsync(string checkString)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Learner>> GetLearnersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateLearnerAsync(LearnerDTO.CreateRequestModel requestModel, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

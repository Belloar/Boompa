using Boompa.Context;
using Boompa.DTO;
using Boompa.Entities;
using Boompa.Entities.Identity;
using Boompa.Exceptions;
using Boompa.Interfaces.IRepository;
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

        public async Task<int> AddLearner(Learner model, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _context.Learners.AddAsync(model, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result;
        }
        public async Task<int> DeleteLearner(LearnerDTO.DeleteModel model, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var learner = _context.Learners.FirstOrDefault(x => x.UserId == model.UserId ) ;
            if (learner == null) throw new ServiceException("this user is not a learner");
            learner.IsDeleted = model.IsDeleted;
            learner.DeletedOn = model.DeletedOn;
            learner.DeletedBy = model.Deletedby;

            _context.Learners.Update(learner);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Learner> GetLearner(int id)
        {
            var learner =  _context.Learners.FirstOrDefault(l => l.UserId == id);
            if (learner == null || learner.IsDeleted == true) throw new ServiceException("a learner with this username or email address does not exist");
            return learner;
        }
        public Task<IEnumerable<Learner>> GetLearners(bool byStatus = false)
        {
            throw new NotImplementedException();
        }
        public Task<int> UpdateLearner(int learnerId, LearnerDTO.UpdateRequestModel model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateLearner(int Userid, LearnerDTO.UpdateStatsModel model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

using Boompa.Context;
using Boompa.DTO;
using Boompa.Entities;
using Boompa.Entities.Identity;
using Boompa.Exceptions;
using Boompa.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Boompa.Implementations.Repositories
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
        public async Task<Learner> GetLearner(string searchString)
        {
            if (searchString.Contains('@'))
            {
                var _result = _context.Learners.FirstOrDefault(x => x.User.Email == searchString);
            }
            var result = _context.Learners.Include(d => d.Diary).ThenInclude(v => v.Visit).FirstOrDefault(x => x.User.UserName == searchString);
            if (result == null) throw new IdentityException("this user does not exist or has been deleted");
            return result;
        }
        public async Task<IEnumerable<Learner>> GetLearners(bool byStatus = false)
        {
            return byStatus ? _context.Learners.Where(l => l.Status == true).ToList() : _context.Learners.ToList(); 
        }
        public async Task<int> UpdateLearner(Learner learner, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _context.Update(learner);
            var result = _context.SaveChanges();
            return result;
            

        }

        
    }
}

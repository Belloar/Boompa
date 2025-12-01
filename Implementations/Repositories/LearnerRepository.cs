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
        private readonly BoompaContext _context;
        public LearnerRepository(BoompaContext context)
        {
            _context = context;
        }

        

        public async Task AddLearner(Learner model)
        {
            await _context.Learners.AddAsync(model);
        }
        public async Task<int> DeleteLearner(LearnerDTO.DeleteModel model)
        {
            

            var learner = _context.Learners.FirstOrDefault(l => l.Id == model.UserId ) ;
            if (learner == null) throw new ServiceException("this user is not a learner");
            learner.IsDeleted = model.IsDeleted;
            learner.DeletedOn = model.DeletedOn;
            learner.DeletedBy = model.Deletedby;

            _context.Learners.Update(learner);
            return await _context.SaveChangesAsync();
        }

        public async Task<Learner> GetLearner(Guid id)
        {
            var learner =  _context.Learners.FirstOrDefault(l => l.Id == id);
            if (learner == null || learner.IsDeleted == true) throw new ServiceException("a learner with this username or email address does not exist");
            return learner;
        }
        public async Task<Learner> GetLearner(string searchString)
        {
            return await _context.Learners.FirstOrDefaultAsync(x => x.Email == searchString);
        }

        public async Task<IEnumerable<Learner>> GetLearners(int skipCount)
        {
            var data = _context.Learners.AsQueryable();

            var recordCount = await data.CountAsync();

            var result = data.Skip(skipCount).Take(skipCount + 50);

             return result.Select(l => new Learner
                    {
                        FirstName = l.FirstName,
                        LastName = l.LastName,
                        Age = l.Age,
                                        
                    }

             ).ToList();

             
        }

        public async Task<IEnumerable<Learner>> GetLearners(bool byStatus = false)
        {
            if (byStatus)
            {
               var result =  await _context.Learners.Where(l => l.Status == true).ToListAsync();
                return result;
            }
            else
            {
                var result = await _context.Learners.ToListAsync();
                return result;
            }
        }

        public async Task UpdateLearner(Learner learner)
        {
             _context.Update(learner);
        }

        
    }
}

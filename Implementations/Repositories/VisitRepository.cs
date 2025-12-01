using Boompa.Context;
using Boompa.Entities;
using Boompa.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Boompa.Implementations.Repositories
{
    public class VisitRepository : IVisitRepository
    {
        private readonly BoompaContext _context;
        public VisitRepository(BoompaContext context)
        {
            _context = context;
        }
        public async Task AddVisit(Visit visit)
        {
            await _context.Visits.AddAsync(visit);
        }

        public Task<Visit> GetVisit()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Visit>> GetVisitsByCategory(Guid categoryId)
        {
            return await _context.Visits.Where(v => v.CategoryId ==  categoryId).Select(v => v).ToListAsync();
        }

        public async Task<ICollection<Visit>> GetVisitsByDate(DateOnly date)
        {
            return await _context.Visits.Where(v => v.Date == date).Select(v => v).ToListAsync();
        }

        public async Task<ICollection<Visit>> GetVisitsByLearner(Guid learnerId)
        {
            return await _context.Visits.Where(v => v.LearnerId == learnerId).Select(v => v).ToListAsync();
        }
    }
}

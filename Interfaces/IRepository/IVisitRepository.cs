using Boompa.Entities;

namespace Boompa.Interfaces.IRepository
{
    public interface IVisitRepository
    {
        Task AddVisit(Visit visit);
        Task<Visit> GetVisit();
        Task<ICollection<Visit>> GetVisitsByLearner(Guid LearnerId);
        Task<ICollection<Visit>> GetVisitsByDate(DateOnly date);
        Task<ICollection<Visit>> GetVisitsByCategory(Guid CategoryId);
    }
}

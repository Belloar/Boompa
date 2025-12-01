using Boompa.Entities;

namespace Boompa.Interfaces.IService
{
    public interface IVisitService
    {
        Task AddVisit(Visit visit);
        Task<Visit> GetVisit();
        ICollection<Task<Visit>> GetVisits(int diaryId);
        ICollection<Task<Visit>> GetVisits(string date);
    }
}

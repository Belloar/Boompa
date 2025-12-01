using Boompa.Entities;
using Boompa.Interfaces;
using Boompa.Interfaces.IService;

namespace Boompa.Implementations.Services
{
    public class VisitService : IVisitService
    {
        private readonly IUnitOfWork _unitOfWork;
        public VisitService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddVisit(Visit visit)
        {
            await _unitOfWork.Visits.AddVisit(visit);
        }

        public Task<Visit> GetVisit()
        {
            throw new NotImplementedException();
        }

        public ICollection<Task<Visit>> GetVisits(int diaryId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Task<Visit>> GetVisits(string date)
        {
            throw new NotImplementedException();
        }
    }
}

using Boompa.DTO;
using Boompa.Entities;

namespace Boompa.Interfaces.IRepository
{
    public interface IContestRecordRepository
    {
        Task AddRecord(ContestRecord record);
        Task<ICollection<ContestRecord>> GetAllRecords();
        Task<ContestRecord> GetIfExists(DateOnly date, Guid learnerId, Guid categoryId);
        Task<ICollection<ContestRecord>> GetRecordsByMonth(DateOnly date);
        Task UpdateLearnerRecord(ContestRecord record);
    }
}

using Boompa.DTO;
using Boompa.Entities;

namespace Boompa.Interfaces.IRepository
{
    public interface IContestRecordRepository
    {
        Task AddRecord(ContestRecord record);
        Task<ICollection<ContestRecord>> GetAllRecords();
        Task<ICollection<ContestRecord>> GetRecordsByMonth(DateOnly date);
         
    }
}

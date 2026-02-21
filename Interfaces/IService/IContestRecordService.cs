using Boompa.DTO;
using Boompa.Entities;

namespace Boompa.Interfaces.IService
{
    public interface IContestRecordService
    {
        Task AddRecord(ContestRecordDTO.CreateRecord model, string email);
        Task<ICollection<ContestRecordDTO.ReturnRecord>> GetAllRecords();
        Task<ICollection<ContestRecordDTO.ReturnRecord>> GetRecordsByMonth(DateOnly date);
        Task UpdateRecord(ContestRecord record, ContestRecordDTO.CreateRecord model);
        Task UpdateRecord(ContestRecordDTO.UpdateRecord model, string email);
    }
}

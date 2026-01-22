using Boompa.DTO;

namespace Boompa.Interfaces.IService
{
    public interface IContestRecordService
    {
        Task AddRecord(ContestRecordDTO.CreateRecordDTO record);
        Task<ICollection<ContestRecordDTO.ReturnRecordDTO>> GetAllRecords();
        Task<ICollection<ContestRecordDTO.ReturnRecordDTO>> GetRecordsByMonth(DateOnly date);
    }
}

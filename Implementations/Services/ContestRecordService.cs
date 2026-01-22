using Boompa.DTO;
using Boompa.Entities;
using Boompa.Interfaces;
using Boompa.Interfaces.IService;

namespace Boompa.Implementations.Services
{
    public class ContestRecordService : IContestRecordService
    {
        private readonly IUnitOfWork  _unitOfWork;

        public ContestRecordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task AddRecord(ContestRecordDTO.CreateRecordDTO record)
        {
            throw new NotImplementedException();
            //try
            //{

            //    var newRecord = new ContestRecord
            //    {
            //        ChallengerId = record.ChallengerId,
            //        CategoryId = record.CategoryId,
            //        SpeedAccuracyRatio = record.SpeedAccuracyRatio,
            //        Date = record.Date,
            //        ExpEarned = record.ExpEarned,
            //        NumberOfRounds = record.NumberOfRounds
            //    };
            //}
            //catch (Exception ex )
            //{
                
            //}
        }

        public async Task<ICollection<ContestRecordDTO.ReturnRecordDTO>> GetAllRecords()
        {
            //throw new NotImplementedException();
            try
            {
                var result = new List<ContestRecordDTO.ReturnRecordDTO>();

                var records = await _unitOfWork.ContestRecords.GetAllRecords();
                foreach (var record in records)
                {
                    var recordDto = new ContestRecordDTO.ReturnRecordDTO(
                        record.SpeedAccuracyRatio,
                        record.Date,
                        record.ExpEarned,
                        record.NumberOfRounds
                    );
                    result.Add( recordDto );
                }
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<ICollection<ContestRecordDTO.ReturnRecordDTO>> GetRecordsByMonth(DateOnly date)
        {
            throw new NotImplementedException();
        }
    }
}

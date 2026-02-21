using Boompa.DTO;
using Boompa.Entities;
using Boompa.Exceptions;
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
        public async Task AddRecord(ContestRecordDTO.CreateRecord model, string email)
        {
            try
            {
                var learner = await _unitOfWork.Learners.GetLearner(email);
                var category = await _unitOfWork.SourceMaterials.GetCategoryId(model.CategoryName);

                var record = await _unitOfWork.ContestRecords.GetIfExists(model.Date, learner.Id, category.Id);

                if (record == null)
                {
                    var newRecord = new ContestRecord
                    {
                        LearnerId = learner.Id,
                        CategoryId = category.Id,
                        SpeedAccuracyRatio = model.SpeedAccuracyRatio,
                        Date = model.Date,
                        LastModifiedOn = model.LastModifiedOn,
                        ExpEarned = model.ExpEarned,
                        NumberOfRounds = model.NumberOfRounds
                    };

                    await _unitOfWork.ContestRecords.AddRecord(newRecord);
                }
                else
                {
                    await UpdateRecord(record,model);
                }
                
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        public async Task<ICollection<ContestRecordDTO.ReturnRecord>> GetAllRecords()
        {
            try
            {
                var result = new List<ContestRecordDTO.ReturnRecord>();

                var records = await _unitOfWork.ContestRecords.GetAllRecords();
                foreach (var record in records)
                {
                    var recordDto = new ContestRecordDTO.ReturnRecord(
                        record.LearnerId.ToString(),
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

        public Task<ICollection<ContestRecordDTO.ReturnRecord>> GetRecordsByMonth(DateOnly date)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateRecord(ContestRecord record, ContestRecordDTO.CreateRecord model)
        {
            try
            {
                record.SpeedAccuracyRatio = model.SpeedAccuracyRatio;
                record.LastModifiedOn = model.LastModifiedOn;
                record.NumberOfRounds += model.NumberOfRounds;
                record.ExpEarned += model.ExpEarned;

                await _unitOfWork.ContestRecords.UpdateLearnerRecord(record);

            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        public Task UpdateRecord(ContestRecordDTO.UpdateRecord model, string email)
        {
            throw new NotImplementedException();
        }
    }
}

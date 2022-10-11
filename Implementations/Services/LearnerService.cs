using Boompa.Auth;
using Boompa.DTO;
using Boompa.Entities;
using Boompa.Exceptions;
using Boompa.Interfaces.IRepository;
using Boompa.Interfaces.IService;


namespace Boompa.Services
{
    public class LearnerService : ILearnerService
    {
        private readonly ILearnerRepository _learnerRepo;
        private readonly IIdentityService _identityService;
        public LearnerService(ILearnerRepository repository,IIdentityService identityService)
        {
            _learnerRepo = repository;
            _identityService = identityService;
        }
        public Task<IEnumerable<SourceMaterial>> ConversationCompiler(string MaterialName)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateLearner(LearnerDTO.CreateRequestModel model, CancellationToken cancellationToken)
        {
            
            var learner = new Learner
            {
                UserId = model.UserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Age = model.Age,
                CreatedBy = $"{model.FirstName} {model.LastName}"

            };
            var result = await _learnerRepo.AddLearner(learner, cancellationToken);
            return result;
        }
        public async Task<int> DeleteLearner(int id, CancellationToken cancellationToken)
        {
            var user = await _identityService.GetUserAsync(id);
            if (user == null) throw new IdentityException("a user with this username or email address does not exist");
            var deleteModel = new LearnerDTO.DeleteModel()
            {
                UserId = id,
                IsDeleted = true,
                Deletedby = user.UserName,
                DeletedOn = DateTime.UtcNow,
            };
            var result = await _learnerRepo.DeleteLearner(deleteModel, cancellationToken);
            return result;
        }

        public Task<Learner> GetLearner(int id)
        {
            var result = _learnerRepo.GetLearner(id);
            if (result == null) return null;
            return result;
        }

        public async Task<Learner> GetLearner(string checkString)
        {
            var user = await _identityService.GetUserAsync(checkString);
            if (user == null) throw new IdentityException("a user with this username or email does not exist");
            var learner = await _learnerRepo.GetLearner(user.Id);
            if (learner == null)  throw new ServiceException("this user is not a learner");
            return learner;
        }

        public Task<IEnumerable<Learner>> GetLearners()
        {
            throw new NotImplementedException();
        }

        public Task<SourceMaterial> GetMaterial(string MaterialName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SourceMaterial>> LoadCategoryMaterials(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Task<Question> QuestionSelector()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateLearner(int learnerId, LearnerDTO.UpdateRequestModel model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateLearner(int Userid, LearnerDTO.UpdateStatsModel model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

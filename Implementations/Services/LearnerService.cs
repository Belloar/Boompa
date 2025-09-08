using Boompa.Auth;
using Boompa.DTO;
using Boompa.Entities;
using Boompa.Entities.Identity;
using Boompa.Exceptions;
using Boompa.Interfaces.IRepository;
using Boompa.Interfaces.IService;


namespace Boompa.Services
{
    public class LearnerService : ILearnerService
    {
        private readonly ILearnerRepository _learnerRepository;
        private readonly IIdentityRepository _identityRepository;
        public LearnerService(ILearnerRepository repository,IIdentityRepository identityRepository)
        {
            _learnerRepository = repository;
            _identityRepository = identityRepository;
        }
        public Task<IEnumerable<SourceMaterial>> ConversationCompiler(string MaterialName)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateLearner(LearnerDTO.CreateRequest model, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var role1 = await _identityRepository.GetRoleAsync("user");
            var role2 = await _identityRepository.GetRoleAsync("learner");
            if (await _identityRepository.CheckUser(model.Email)) throw new IdentityException("a user with this email already exists");

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                CreatedBy = model.UserName,
                Hashsalt = BCrypt.Net.BCrypt.GenerateSalt(),
                IsEmailConfirmed = true,
                

            };
            user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password, user.Hashsalt);
            await _identityRepository.CreateAsync(user, cancellationToken);
            user.Roles = new HashSet<UserRole>()
            {
                new UserRole
                {
                    User = user,
                    RoleId = role1.Id,
                    Role = role1,
                    UserId = user.Id
                },
                new UserRole
                {
                    Role = role2,
                    RoleId = role2.Id,
                    User = user,
                    UserId = user.Id
                }
            };
            await _identityRepository.AddUserRole(user.Roles , cancellationToken);
            var diary = new Diary()
            {
                UserId = user.Id,
                User = user
            };
            var learner = new Learner
            {
                UserId = user.Id,
                Age = model.Age,
                DiaryId = diary.Id,
                Diary = diary,
                CreatedBy = model.UserName,
                CreatedOn = DateTime.UtcNow,
                CoinCount = 100,
                TicketCount = 20
            };

            var result = await _learnerRepository.AddLearner(learner, cancellationToken);
            if (result == 0) throw new ServiceException("unable to add learner");
            return result;
        }
        public async Task<int> DeleteLearner(int id, CancellationToken cancellationToken)
        {
            var user = await _identityRepository.GetUserAsync(id);
            if (user == null) throw new IdentityException("a user with this username or email address does not exist");
            var deleteModel = new LearnerDTO.DeleteModel()
            {
                UserId = id,
                IsDeleted = true,
                Deletedby = user.UserName,
                DeletedOn = DateTime.UtcNow,
            };
            var result = await _learnerRepository.DeleteLearner(deleteModel, cancellationToken);
            return result;
        }

        public async Task<Learner> GetLearner(int id)
        {
            var result = await _learnerRepository.GetLearner(id);
            if (result == null || result.IsDeleted == true) throw new ServiceException("learner not found");
            return result;
        }

        public async Task<LearnerDTO.LearnerInfo> GetLearner(string checkString)
        {
            var user = await _identityRepository.GetUserAsync(checkString);
           
            if (user == null || user.IsDeleted == true) throw new IdentityException("a learner with this username or email does not exist");
            var learner = await _learnerRepository.GetLearner(user.Id);
            var serviceLearner = new LearnerDTO.LearnerInfo
            {
                FirstName = learner.FirstName,
                LastName = learner.LastName,
                Status = learner.Status,
                School = learner.School,
                Rank = learner.Rank,

            };
            if (learner == null)  throw new ServiceException("this learner does not exist");
            return serviceLearner ;
        }

        public async Task<IEnumerable<Learner>> GetLearners()
        {
            var learners = await _learnerRepository.GetLearners();
            return learners;
        }
        public async Task<IEnumerable<LearnerDTO.LearnerInfo>> GetLearnersInfo()
        {
            var learners = await _learnerRepository.GetLearners(true);
            var result = learners.Select(learner => new LearnerDTO.LearnerInfo
            {
                FirstName = learner .FirstName,
                LastName = learner.LastName,
                Status = learner.Status,
                School = learner.School,
                Rank = learner.Rank,
            }).ToList();

            return result;
            
        }

        public Task<SourceMaterial > GetMaterial(string MaterialName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SourceMaterial >> LoadCategoryMaterials(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Task<int> Play()
        {
            throw new NotImplementedException();
        }

        public Task<Question> QuestionSelector()
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateLearner(LearnerDTO.UpdateInfo model, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var learner = await _learnerRepository.GetLearner(model.UserId);

            learner.LastModifiedBy = model.ModifierName;
            learner.LastModifiedOn= DateTime.UtcNow;
            learner.School = model.School;
            learner.FirstName = model.FirstName;
            learner.LastName = model.LastName;

            var result = await _learnerRepository.UpdateLearner(learner, cancellationToken);
            return result;
        }

        public async Task<int> UpdateLearner(LearnerDTO.UpdateStats model, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var learner = await _learnerRepository.GetLearner(model.UserId);
            learner.CoinCount += model.CoinCount;
            learner.TicketCount += model.TicketCount;

            var result = await _learnerRepository.UpdateLearner(learner, cancellationToken);
            return result;
            

        }
    }
}

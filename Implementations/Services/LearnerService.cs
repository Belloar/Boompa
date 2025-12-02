using Boompa.Auth;
using Boompa.DTO;
using Boompa.Entities;
using Boompa.Entities.Identity;
using Boompa.Exceptions;
using Boompa.Interfaces;
using Boompa.Interfaces.IRepository;
using Boompa.Interfaces.IService;


namespace Boompa.Services
{
    public class LearnerService : ILearnerService
    {
        private readonly ILearnerRepository _learnerRepository;
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVisitService _visitService;
        
        
        
        public LearnerService(ILearnerRepository repository,IIdentityRepository identityRepository,IUnitOfWork unitOfWork,IVisitService visitService)
        {
            _learnerRepository = repository;
            _identityRepository = identityRepository;
            _unitOfWork = unitOfWork;
            _visitService = visitService;
            
        }
       

        public async Task<int> CreateLearner(LearnerDTO.CreateRequest model)
        {
            
            
            var role1 = await _identityRepository.GetRoleAsync("User");
            var role2 = await _identityRepository.GetRoleAsync("Learner");


            if (await _identityRepository.CheckUser(model.Email)) throw new IdentityException("a user with this email already exists");

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                CreatedBy = model.UserName,
                HashSalt = BCrypt.Net.BCrypt.GenerateSalt(),
                IsEmailConfirmed = true,
                
            };
            
            user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password, user.HashSalt);
            user.Roles = new HashSet<UserRole>
            {
                new UserRole
                {
                    User = user,
                    RoleId = role1.Id,
                },
                new UserRole
                {
                    User = user,
                    RoleId = role2.Id,
                }
            };


            var learner = new Learner
            {
                Email = model.Email,
                Age = model.Age,
                PhoneNumber = model.PhoneNumber,
                CreatedBy = model.UserName,
                CreatedOn = DateTime.UtcNow,
                CoinCount = 100,
                TicketCount = 20
               
            };
            await _unitOfWork.Identity.AddUserRoles(user.Roles);
            await _unitOfWork.Identity.CreateAsync(user);
            await _unitOfWork.Learners.AddLearner(learner);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result == 0) throw new ServiceException("unable to add learner");
            return result;
        }
        public async Task<int> DeleteLearner(Guid id)
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
            var result = await _learnerRepository.DeleteLearner(deleteModel);
            return result;
        }

        public async Task<Learner> GetLearner(Guid id)
        {
            var result = await _learnerRepository.GetLearner(id);
            if (result == null || result.IsDeleted == true) throw new ServiceException("learner not found");
            return result;
        }

        public async Task<Response> GetLearner(string checkString)
        {
            var response = new Response();
            
            var learner = await _learnerRepository.GetLearner(checkString);
            response.Data = learner;
            
            if (learner == null)  throw new ServiceException("this learner does not exist");
            return response ;
        }

        public async Task<Response> GetLearners(int skipCount)
        {
            var response = new Response();
            var result = new List<LearnerDTO.ReturnLearner>();
            var learners = await _learnerRepository.GetLearners(skipCount);
            if(learners == null) throw new ServiceException("no learners found");

            foreach(var learner in learners)
            {
                
                var lea = new LearnerDTO.ReturnLearner
                {
                    LastName = learner.LastName,
                    FirstName = learner.FirstName,
                    Email = learner.Email,
                    Age = learner.Age,
                    PhoneNumber = learner.PhoneNumber,
                };

                result.Add(lea);
            }
            response.StatusMessages.Add("success");
            response.Data = result;

            return response;
        }
        public async Task<IEnumerable<LearnerDTO.LearnerInfo>> GetLearnersInfo()
        {
            var learners = await _unitOfWork.Learners.GetLearners(false);
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
        public async Task<int> UpdateLearner(LearnerDTO.UpdateInfo model, Guid learnerId)
        {
            
            var learner = await _learnerRepository.GetLearner(learnerId);

            learner.LastModifiedBy = model.ModifierName;
            learner.LastModifiedOn= DateTime.UtcNow;
            learner.School = model.School;
            learner.FirstName = model.FirstName;
            learner.LastName = model.LastName;

            await _unitOfWork.Learners.UpdateLearner(learner);
            var result = await  _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateLearner(LearnerDTO.UpdateStats model, string email)
        {
            //get learner who sent the request and validate if it exists
            var learner = await _learnerRepository.GetLearner(email);
            if (learner == null) { throw new RepoException("database error"); }

            //update learner currencies
            learner.CoinCount += model.CoinCount;
            learner.TicketCount += model.TicketCount;

            //document learner learning session
            await DocumentVisit(model, learner.Id);
            await _unitOfWork.Learners.UpdateLearner(learner);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;

        }
        private async Task DocumentVisit(LearnerDTO.UpdateStats statData,Guid learnerId)
        {
            var visit = new Visit
            {
                LearnerId = learnerId,
                CategoryId = statData.CategoryId,
                TicketsEarned = statData.TicketCount,
                CoinsEarned = statData.CoinCount,
                Duration = statData.Duration,
                Date = statData.Date

            };

            await _visitService.AddVisit(visit);
        }

       
    }
}

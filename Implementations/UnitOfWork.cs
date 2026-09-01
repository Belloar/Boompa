using Boompa.Auth;
using Boompa.Context;
using Boompa.Interfaces;
using Boompa.Interfaces.IRepository;

namespace Boompa.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BoompaContext _context;
        public ILearnerRepository Learners { get; }
        public IAdminRepository Admins { get; }
        public ISourceMaterialRepository SourceMaterials { get; }
        public IIdentityRepository Identity { get; }
        public IVisitRepository Visits { get; }
        public IContestRecordRepository ContestRecords { get; }
        public ICategoryRepository CategoryRecords { get; }

        public UnitOfWork(BoompaContext context, ILearnerRepository learnerRepository, IAdminRepository adminRepository, ISourceMaterialRepository sourceMaterialRepository, IIdentityRepository identityRepository, IVisitRepository visitRepository, IContestRecordRepository contestRecordRepository,ICategoryRepository categoryRecords)
        {
            _context = context;
            SourceMaterials = sourceMaterialRepository;
            Learners = learnerRepository;
            Admins = adminRepository;
            Identity = identityRepository;
            Visits = visitRepository;
            ContestRecords = contestRecordRepository;
            CategoryRecords = categoryRecords;
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();
        }
    }
}

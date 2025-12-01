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
        public IIdentityRepository Identity {  get; }
        public IVisitRepository Visits { get; }
        public UnitOfWork(BoompaContext context,ILearnerRepository learnerRepository, IAdminRepository adminRepository, ISourceMaterialRepository sourceMaterialRepository,IIdentityRepository identityRepository, IVisitRepository visitRepository)
        {
            _context = context;
            SourceMaterials = sourceMaterialRepository;
            Learners = learnerRepository;
            Admins = adminRepository;
            Identity = identityRepository;
            Visits = visitRepository;
        }



        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();
        }
    }
}

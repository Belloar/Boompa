using Boompa.Auth;
using Boompa.Interfaces.IRepository;

namespace Boompa.Interfaces
{
    public interface IUnitOfWork : IDisposable //remove this interface in due time
    {
        ILearnerRepository Learners {  get; }
        IAdminRepository Admins { get; }
        ISourceMaterialRepository SourceMaterials { get; }
        IIdentityRepository Identity { get; }
        IVisitRepository Visits { get; }
        IContestRecordRepository ContestRecords { get; }

        Task<int> SaveChangesAsync();
        void Dispose();
    }
}

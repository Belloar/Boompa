using Boompa.Auth;
using Boompa.Interfaces.IRepository;

namespace Boompa.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ILearnerRepository Learners {  get; }
        IAdminRepository Admins { get; }
        ISourceMaterialRepository SourceMaterials { get; }
        IIdentityRepository Identity { get; }

        Task<int> SaveChangesAsync();
        void Dispose();
    }
}

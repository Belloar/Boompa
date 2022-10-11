using Boompa.Entities;

namespace Boompa.Interfaces.IRepository
{
    public interface ICategoryRepository
    {
        Task<int> AddCategory(Category category, CancellationToken cancellationToken);
        Task<int> UpdateCategory(int id , CancellationToken cancellationToken);
    }
}

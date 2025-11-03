using Boompa.Entities;

namespace Boompa.Interfaces.IRepository
{
    public interface ICategoryRepository
    {
        Task<int> AddCategory(Category category);
        Task<int> UpdateCategory(int id ,string updateModel);
        Task<int> DeleteCategory(int id);


    }
}

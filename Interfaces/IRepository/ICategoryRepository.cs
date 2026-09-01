using Boompa.Entities;

namespace Boompa.Interfaces.IRepository
{
    public interface ICategoryRepository
    {
        Task<int> AddCategory(Category category);
        Task<int> UpdateCategory();
        Task<int> DeleteCategory(int id);
        Task AddFavouriteCategory(CategoryLearner categoryLearner);
        Task UpdateFavouriteCategory(CategoryLearner categoryLearner);
        Task<CategoryLearner> GetFavouriteCategory(Guid categoryId, Guid learnerId);


    }
}

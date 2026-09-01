using Boompa.Context;
using Boompa.Entities;
using Boompa.Exceptions;
using Boompa.Interfaces.IRepository;

namespace Boompa.Implementations.Repositories
{
    public class CategoryRepository(BoompaContext context) : ICategoryRepository
    {
        
        public Task<int> AddCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public async Task AddFavouriteCategory(CategoryLearner categoryLearner)
        {
            await context.CategoryLearners.AddAsync(categoryLearner);
        }

        public Task<int> DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryLearner> GetFavouriteCategory(Guid categoryId, Guid learnerId)
        {
            var result = context.CategoryLearners.SingleOrDefault(cl => cl.CategoryId == categoryId && cl.LearnerId == learnerId);
            if (result == null)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        public Task<int> UpdateCategory()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateFavouriteCategory(CategoryLearner categoryLearner)
        {
            context.CategoryLearners.Update(categoryLearner);
        }
    }
}

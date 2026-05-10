using Boompa.Context;
using Boompa.DTO;
using Boompa.Entities;
using Boompa.Exceptions;
using Boompa.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Boompa.Implementations.Repositories
{
    public class SourceMaterialRepository : ISourceMaterialRepository
    {
        private readonly BoompaContext _context;
        public SourceMaterialRepository(BoompaContext context)
        {
            _context = context;
        }

       

       
        //public async Task<Question> AddQuestionAsync(Question model,string sourceName,string category)
        //{
        //    var source = await GetSource(sourceName, category);
        //    model.SourceMaterialId = source.Id;
            
        //    _context.Questions.Add(model);
        //    return model;

        //}
        public async Task<Question> AddQuestionAsync(Question model)
        {
            _context.Questions.Add(model);
            return model;

        }

        public async Task<SourceMaterial> AddSourceMaterial(SourceMaterial sourceMaterial)
        {
            var canConnect = await _context.Database.CanConnectAsync();
            Console.WriteLine(canConnect);

            await _context.SourceMaterials.AddAsync(sourceMaterial);
            return sourceMaterial;
            
        }
        

        public Task DeleteQuestionAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteSourceMaterial()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<MaterialDTO.SourceDescriptor>> GetAll(int skipCount)
        {

            var fetcher = _context.SourceMaterials.Skip(skipCount*50).Take(50).Select(sm => new MaterialDTO.SourceDescriptor
            {
                SourceId = sm.Id,
                SourceName = sm.Name,
                SourceDescription = sm.Description,
                Categories = sm.Categories.Select(csm => new MaterialDTO.CategoryDetails
                {
                    CategoryId = csm.CategoryId,
                    Name = csm.Category.Name
                }).ToList()
            }).ToList();
            if (fetcher == null)
            {
                throw new RepoException("failed to get materials");
            }
            return fetcher;
        }

        public async Task<ICollection<MaterialDTO.SourceDescriptor>> GetAll(Guid categoryId,int skipCount)
        {

            var fetcher = await _context.CategorySourceMaterials
                .Where(csm => csm.CategoryId == categoryId)
                .Skip(skipCount*50).Take(50)
                .Select(csm => new MaterialDTO.SourceDescriptor
                {
                    SourceId = csm.SourceMaterialId,
                    SourceName = csm.SourceMaterial.Name,
                    SourceDescription = csm.SourceMaterial.Description,
                    Categories = csm.SourceMaterial.Categories
                    .Select(cat => new MaterialDTO.CategoryDetails
                    {
                        CategoryId = cat.CategoryId,
                        Name = cat.Category.Name
                    }).ToList()
                }).ToListAsync();
            if (fetcher == null)
            {
                throw new RepoException("failed to get materials");
            }
            return fetcher;
        }

        public Task<SourceMaterial> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<CategorySourceMaterial>> GetByCategory(Guid Id)
        {
            var result = await _context.CategorySourceMaterials.Include(csm => csm.SourceMaterial).Where(csm => csm.CategoryId == Id).ToListAsync();
            if (result == null)
            {
                throw new RepoException("An error occured while fetching materials");
            }
            return result;
        }
        public async Task<SourceMaterial> GetRandomSource()
        {
            var rand = new Random();

            var data = _context.SourceMaterials.AsQueryable();
            var size = await data.CountAsync();

            if(size<= 0)
            {
                throw new RepoException("No articles available");
            }
            var response = _context.SourceMaterials.Skip(rand.Next(0, size - 1)).Take(1);
            var result = response.Single();
            return result;
        }

        public async Task<SourceMaterial> GetSourceMaterial(string sourceMaterialName, string category)
        {

            var result = await _context.SourceMaterials
                .Include(sm => sm.Questions)
                .FirstOrDefaultAsync(sm => sm.Name.ToLower() == sourceMaterialName.ToLower() && !sm.IsDeleted);

            if (result == null) { throw new RepoException("No material found with the provided name"); }
            return result;
        }

        public async Task<SourceMaterial> GetSourceMaterial(Guid sourceId)
        {
            var result = await _context.SourceMaterials.Include(sm => sm.Categories).ThenInclude(csm => csm.Category).Include(sm => sm.Questions).FirstOrDefaultAsync(sm => sm.Id == sourceId && !sm.IsDeleted);
            return result;

        }



        public Task UpdateQuestion()
        {
            throw new NotImplementedException();
        }

        //private async Task<SourceMaterial> GetSource(string sourceName,string category)
        //{
        //    var result = await _context.SourceMaterials.Select(sm => new SourceMaterial
        //    {
        //        Id = sm.Id,
        //    }).FirstOrDefaultAsync(sm => sm.Name == sourceName && sm.Category.Name == category);
            
        //    return result;
        //}

        public async Task<bool> CategoryExists(string categoryName)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == categoryName.ToLower());
            if(result != null) { return true; }else{ return false; }
        }

        public async Task<Category> GetCategoryId(string categoryName)
        {
            
            var res = await _context.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == categoryName.ToLower());
            return res;


        }

        public async Task AddCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task<ICollection<MaterialDTO.CategoryDetails>> GetTopCategories(string learnerId)
        {
            var learner = await GetLearnerAsync(learnerId);
            var response = await _context.CategoryLearners.Where(cl => cl.LearnerId == learner.Id).OrderByDescending(cl => cl.ReadCount).Take(7).Select(cl => new MaterialDTO.CategoryDetails
            {
                CategoryId = cl.CategoryId,
                Name = cl.Category.Name
            }).ToListAsync();

            return response;
        }

        private async Task<Learner> GetLearnerAsync(string learnerId)
        {
            var learner = await _context.Learners.FirstOrDefaultAsync(l => l.Email == learnerId);
            var result = learner == null ? throw new RepoException("user doesn't exist") : learner;
            return result;
        }

        public async Task<ICollection<MaterialDTO.CategoryDetails>> GetCategories()
        {
            var result = await _context.Categories.Select(cat => new MaterialDTO.CategoryDetails
            {
                CategoryId = cat.Id,
                Name = cat.Name,

            }).ToListAsync();
            return result;
        }

        public async Task<Category> GetCategory(Guid Id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == Id);
            if(category == null)
            {
                throw new RepoException("Category not found");
            }
            return category;
        }
    }
}
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

       

        public Task AddChallengeAsync()
        {
            throw new NotImplementedException();
        }

        //public async Task AddFileDetail(List<SourceFileDetail> files)
        //{
        //    foreach (SourceFileDetail fileDeets in files)
        //    {
        //         _context.SourceFileDetails.Add(fileDeets);
        //    }
            

        //}
        //public async Task AddFileDetail(List<QuestionFileDetail> files)
        //{
        //    foreach (var fileDeets in files)
        //    {
        //         _context.QuestionFileDetails.Add(fileDeets);
        //    }
            
        //}
        public async Task<Question> AddQuestionAsync(Question model,string sourceName,string category)
        {
            var source = await GetSource(sourceName, category);
            model.SourceMaterialId = source.Id;
            
            _context.Questions.Add(model);
            return model;

        }

        public async Task<SourceMaterial> AddSourceMaterial(SourceMaterial sourceMaterial)
        {
            _context.SourceMaterials.Add(sourceMaterial);
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

        public async Task<ICollection<MaterialDTO.SourceDescriptor>> GetAllSourceMaterials(string categoryName)
        {
            
            var fetcher =_context.SourceMaterials.Where(sm => sm.Category.Name == categoryName).Select(sm => new MaterialDTO.SourceDescriptor
            {
                SourceName = sm.Name,
                SourceDescription = sm.Description,
            }).ToList();
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

        public async Task<SourceMaterial> GetSourceMaterial(string sourceMaterialName, string category)
        {

            var result = await _context.SourceMaterials
                .Select(a => new SourceMaterial 
                { 
                    Category = a.Category,
                    Content = a.Content,
                    Id = a.Id,
                    Description = a.Description,
                    Name = a.Name,
                    IsDeleted = a.IsDeleted,
                    Questions = a.Questions!.Select(b => new Question
                    {
                        IsDeleted = b.IsDeleted,
                        Id = b.Id,
                        Description= b.Description,
                        Options = b.Options,
                        Answer = b.Answer,
                        SourceMaterialId = b.SourceMaterialId,
                        
                    }).ToList(),
                   
                })
                .FirstOrDefaultAsync(sm => sm.Name.ToLower() == sourceMaterialName.ToLower() && sm.Category.Name == category);

            if (result == null) { throw new RepoException("No material found with the provided name"); }
            return result;
        }



        public Task UpdateQuestion()
        {
            throw new NotImplementedException();
        }

        private async Task<SourceMaterial> GetSource(string sourceName,string category)
        {
            var result = await _context.SourceMaterials.Select(sm => new SourceMaterial
            {
                Id = sm.Id,
            }).FirstOrDefaultAsync(sm => sm.Name == sourceName && sm.Category.Name == category);
            
            return result;
        }

        public async Task<bool> CategoryExists(string categoryName)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == categoryName.ToLower());
            if(result != null) { return true; }else{ return false; }
        }

        public async Task<Category> GetCategory(string categoryName)
        {
            return await _context.Categories.Select(c => new Category
            {
                Id = c.Id,
            }).FirstOrDefaultAsync(c => c.Name.ToLower() == categoryName.ToLower());
        }
    }
}
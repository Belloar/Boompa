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
        private readonly ApplicationContext _context;
        public SourceMaterialRepository(ApplicationContext context)
        {
            _context = context;
        }

        

        public Task<int> AddChallengeAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddFileDetail(List<SourceFileDetail> files)
        {
            foreach(SourceFileDetail fileDeets in files)
            {
                await _context.SourceFileDetails.AddAsync(fileDeets);
            }
            return _context.SaveChanges();

        }
        public async Task<int> AddFileDetail(List<QuestionFileDetail> files)
        {
            foreach (var fileDeets in files)
            {
                await _context.QuestionFileDetails.AddAsync(fileDeets);
            }
            return _context.SaveChanges();
        }

        public async Task<int> AddOptionAsync(IEnumerable<Option> options)
        {
            foreach(var option in options)
            {
               await _context.Options.AddAsync(option);
            }
            return _context.SaveChanges();
        }

        public async Task<int> AddQuestionAsync(Question model)
        {
             _context.Questions.Add(model);
             var result = _context.SaveChanges();
            if (result != 1) { throw new RepoException("Failed to add question to database"); }
            var questionId =  _context.Questions.First(q => q.SourceMaterialId == model.SourceMaterialId).Id;
            return questionId;        

        }

        public async Task<int> AddSourceMaterial(SourceMaterial sourceMaterial)
        {
            _context.SourceMaterials.Add(sourceMaterial);
            var save =_context.SaveChanges();
            if (save == 1)
            {
                var result = _context.SourceMaterials.First(x => x.Name.ToLower() == sourceMaterial.Name.ToLower()).Id;
                return result;
            }
            else
            {
                return 0;
            }
        }

        public Task<int> DeleteQuestionAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteSourceMaterial()
        {
            throw new NotImplementedException();
        }

        public Task<SourceMaterial> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<SourceMaterial> GetSourceMaterial(string sourceMaterialName,string category)
        {
            
            var result = _context.SourceMaterials.Include(sm => sm.Questions).FirstOrDefault(sm => sm.Name.ToLower() == sourceMaterialName.ToLower());
            if (result == null) { throw new RepoException("No material found with the provided name"); }
            return result;
        }

        public Task<int> UpdateQuestion()
        {
            throw new NotImplementedException();
        }
    }
}

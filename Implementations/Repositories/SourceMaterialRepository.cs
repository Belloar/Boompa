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

        public async Task AddCloudSourceFile(CloudSourceFileDetails cloudFile)
        {
             _context.CloudSourceFileDetails.Add(cloudFile);
        }
        public async Task AddCloudEvalFile(CloudEvalFileDetails evalFile)
        {
            _context.CloudEvalFiles.Add(evalFile);
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
        public async Task<Question> AddQuestionAsync(Question model)
        {
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
            var result = new List<MaterialDTO.SourceDescriptor>();
            var fetcher = await Task.FromResult<ICollection<MaterialDTO.SourceDescriptor>>(_context.SourceMaterials.Where(sm => sm.Category == categoryName).Select(sm => new MaterialDTO.SourceDescriptor
            {
                SourceName = sm.Name,
                SourceDescription = sm.Description,
            }).ToList());
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

            var result = _context.SourceMaterials.Include(sm => sm.CloudSourceFileDetails).Include(s => s.Questions).ThenInclude(q => q.CloudEvalFileDetails).FirstOrDefault(sm => sm.Name.ToLower() == sourceMaterialName.ToLower());
            if (result == null) { throw new RepoException("No material found with the provided name"); }
            return result;
        }



        public Task UpdateQuestion()
        {
            throw new NotImplementedException();
        }

        private async Task<int> GetSourceId(string sourceName)
        {
            var result = _context.SourceMaterials.First(sm => sm.Name == sourceName).Id;
            return result;
        }
    }
}
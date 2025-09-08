using Boompa.Context;
using Boompa.DTO;
using Boompa.Entities;
using Boompa.Interfaces.IRepository;

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

        public async Task<int> AddFileDeets(List<SourceFileDetail> files)
        {
            foreach(SourceFileDetail fileDeets in files)
            {
                await _context.FileDetails.AddAsync(fileDeets);
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

        public async Task<Question> AddQuestionAsync(Question model)
        {
            await _context.Questions.AddAsync(model);
            await _context.SaveChangesAsync();
           var question =  _context.Questions.First(q => q.SourceMaterialId == model.SourceMaterialId);
            return question;        

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

        public Task<int> UpdateQuestion()
        {
            throw new NotImplementedException();
        }
    }
}

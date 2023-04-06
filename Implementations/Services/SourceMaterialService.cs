using Boompa.DTO;
using Boompa.Entities;
using Boompa.Interfaces.IRepository;
using Boompa.Interfaces.IService;
using System.Text;

namespace Boompa.Implementations.Services
{
    public class SourceMaterialService : ISourceMaterialService
    {
        private readonly ISourceMaterialRepository _sourceMatRepository;
        public SourceMaterialService(ISourceMaterialRepository sourceMatRepository)
        {
            _sourceMatRepository = sourceMatRepository;
        }
        public Task<int> AddSourceMaterial(MaterialDTO material)
        {
            var  deetsCollection = new List<FileDeets>();
            var relPath = "~/AppFiles";
            Directory.CreateDirectory(relPath);

            
            
            foreach (var file in material.RawMaterials)
            {
                if(Path.GetExtension(file.FileName) == ".txt")
                {
                    using (var fs = new FileStream(relPath + $"/{file.Name}", FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        var content = material.Text;
                        byte[] buffer = Encoding.UTF8.GetBytes(content);
                        fs.Write(buffer, 0, buffer.Length);
                    }
                }
                var model = new FileDeets()
                {
                    SourceMaterialId = 
                    Path = Path.Combine(relPath, file.FileName),
                    Description = $"{GetMediaTypeFromExtension(Path.GetExtension(file.FileName))}/{material.SourceName}/articlepic/"
                };
                deetsCollection.Add(model);
               using(var fs = new FileStream(relPath+$"/{file.Name}" , FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    file.CopyToAsync(fs);
                }
            }
            _sourceMatRepository.AddFileDeets(deetsCollection);
        }

        public Task<int> DeleteSourceMaterial()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateQuestion(MaterialDTO.QuestionModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateSourceMaterial(MaterialDTO rawMaterial)
        {
            throw new NotImplementedException();
        }
        private Task<int> AddOptions(MaterialDTO.OptionModel model)
        {
            throw new NotImplementedException();
        }

        private Task<int> AddQuestion(MaterialDTO.QuestionModel model)
        {
            throw new NotImplementedException();
        }
        private string GetMediaTypeFromExtension(string extension)
        {
            throw new NotImplementedException();
        }
        private Task<int> CreateNewSource(string sourceName,)
    }
}

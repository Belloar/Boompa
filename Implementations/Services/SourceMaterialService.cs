using Boompa.DTO;
using Boompa.Entities;
using Boompa.Exceptions;
using Boompa.Interfaces.IRepository;
using Boompa.Interfaces.IService;
using Elfie.Serialization;
using System.Diagnostics;
using System.IO;

namespace Boompa.Implementations.Services
{
    public class SourceMaterialService : ISourceMaterialService
    {
        private readonly ISourceMaterialRepository _sourceMatRepository;
        /*private readonly Dictionary<string, string> MediaTypes = new Dictionary<string, string>()
        {
            [".txt"] = "texts",
            [".pdf"] = "texts",
            [".doc"] = "texts",
            [".mp3"] = "Audio",
            [".m4a"] ="Audio",
            [".aac"] ="Audio",
            [".wma"] ="Audio",
            [".wav"] ="Audio",
            [".flac"] ="Audio",
            [".dts"] = "Audio",
            [".aiff"] = "Audio",
            [".webm"] = "video",
            [".mpg"] ="video",
            [".mp2"] = "video",
            [".m4p"] ="video",
            [".mp4"] ="video",
            [".wmv"] ="video",
            [".mov"] = "video",
            [".avi"] ="video",
            [".png"] ="image",
            [".jpeg"] ="image",
            [".jpg"] ="image",
            [".gif"] = "image",
            [".tiff"] ="image"


        };*/

        

        public SourceMaterialService(ISourceMaterialRepository sourceMatRepository)
        {
            _sourceMatRepository = sourceMatRepository;    /*********/
        }
        
        public async Task<int> AddSourceMaterial(MaterialDTO.ArticleModel material)//, ICollection<MaterialDTO.QuestionModel> queModel
        {
            var result = 0;
            //the source material object is created
            var sourceMaterial = new SourceMaterial();
            if (material!= null)
            {
                sourceMaterial.Name = material.SourceMaterialName;
                sourceMaterial.Category = material.Category;
                sourceMaterial.Content = material.Text;
                sourceMaterial.CreatedBy = material.Creator;
                sourceMaterial.CreatedOn = material.CreatedOn;
            }
            else
            {
                throw new ServiceException("No source material was provided");
            }
            if (material.RawFiles == null)
            {
                throw new ServiceException("sourceFiles not received");
            }

            var sourceMaterialId = await _sourceMatRepository.AddSourceMaterial(sourceMaterial);
            if (sourceMaterialId <= 0) { throw new ServiceException("Failed to add source material to database"); }
            var fileResult = await AddFileDetails(material.RawFiles, sourceMaterialId);
            
            

                return result;
        }

        public Task<int> DeleteSourceMaterial()
        {





            throw new NotImplementedException();
        }

        public Task<int> UpdateQuestion(string model)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateSourceMaterial(MaterialDTO rawMaterial)
        {
            
            throw new NotImplementedException();
        }


        public async Task<int> AddQuestion(ICollection<MaterialDTO.QuestionModel> model,int sourceMaterialId)
        {
            try
            {
                var result = 0;
                foreach (var question in model)
                {
                    var que = new Question()
                    {
                        Description = question.Description,
                        Answer = question.Answer,
                        SourceMaterialId = sourceMaterialId
                    };
                    
                    result = await _sourceMatRepository.AddQuestionAsync(que);
                    if (result <= 0) { throw new ServiceException("Failed to add question to database"); }
                    if (question.QueFiles != null)
                    {
                        var fileResult = await AddFileDetails(question.QueFiles, result, false);
                        if(fileResult <= 0) { throw new ServiceException("Failed to add question file details to database"); }
                    }
                }
                return result;

            }catch(Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        







        //Description:  Responsible for adding any media file to the to the local file system and sends the file's information to the repository
        //
        //Parameters: files sent from the front end, id(either for question or sourcematerial, forSource-used to detect whether the id is for question or sourcematerial)
        //
        //
        //returns: 
        private async Task<int> AddFileDetails(ICollection<IFormFile> rawFiles,int Id,bool? forSource=true) 
        {
            var result = 0;
            if (rawFiles!= null && rawFiles.Count > 0)
            {
                if (forSource == false)
                {
                    var queFiles = new List<QuestionFileDetail>();
                    foreach (var file in rawFiles)
                    {
                        var prefix = Directory.GetCurrentDirectory();
                        var suffix = Path.Combine(prefix, file.ContentType);
                        var basePath = Path.Combine(prefix, suffix + "/" + file.FileName);
                        Directory.CreateDirectory(suffix);
                        using (var stream = File.Create(basePath))
                        {
                              file.CopyToAsync(stream);
                        }
                        var queFile = new QuestionFileDetail()
                        {
                            QuestionId = Id,
                            FileType = file.ContentType,
                            Path = basePath,
                        };
                        queFiles.Add(queFile);
                    }
                    result = await _sourceMatRepository.AddFileDetail(queFiles);
                }
                else
                {
                    var sourceFiles = new List<SourceFileDetail>();
                    foreach (var file in rawFiles)
                    {
                        var prefix = Directory.GetCurrentDirectory();
                        var suffix = Path.Combine(prefix, file.ContentType);
                        var basePath = Path.Combine(prefix, suffix + "/" + file.FileName);
                        Directory.CreateDirectory(suffix);
                        using (var stream = File.Create(basePath))
                        {
                              file.CopyToAsync(stream);
                        }
                        var sourceFile = new SourceFileDetail()
                        {
                            SourceMaterialId = Id,
                            FileType = file.ContentType,
                            Path = basePath,
                        };
                        sourceFiles.Add(sourceFile);

                    }
                    result = await _sourceMatRepository.AddFileDetail(sourceFiles);

                }
            }

            
            return result;
        }

        public async Task<int> AddCategory(string category)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveFiles(List<SourceFileDetail> deets)
        {

            throw new NotImplementedException();
        }

        
    }
}

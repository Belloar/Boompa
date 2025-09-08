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
        private readonly Dictionary<string, string> MediaTypes = new Dictionary<string, string>()
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


        };

        

        public SourceMaterialService(ISourceMaterialRepository sourceMatRepository)
        {
            _sourceMatRepository = sourceMatRepository;
        }
        
        public async Task<int> AddSourceMaterial(MaterialDTO.ArticleModel material)
        {
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
            else
            {
                await AddFileDetails(material.RawFiles,sourceMaterial.Id,true);
            }
                var sourceMaterialId = await _sourceMatRepository.AddSourceMaterial(sourceMaterial);
            
            //the source material is saved to the database and its id is returned
            
            if(material.Questions != null)
            {
                foreach (var question in material.Questions) 
                {
                    await AddQuestion(question,sourceMaterialId);
                }
            }
            else
            {
                throw new ServiceException("No questions were provided for the source material");
            }


                return 1;
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
        
        
        private async Task<int> AddQuestion(MaterialDTO.QuestionModel model,int sourceMaterialId)
        {
            try
            {
                var question = new Question()
                {
                    SourceMaterialId = sourceMaterialId,
                    Description = model.Description,
                    Answer = model.Answer,
                };
                if(model.RawFiles!= null)
                {
                await AddFileDetails(model.RawFiles, question.Id, false);
                    
                }
                
                var result = await _sourceMatRepository.AddQuestionAsync(question);
                return question.SourceMaterialId;

            }catch(Exception ex)
            {
                throw new ServiceException("Failed to add Question to database");
            }
        }
        /*private async Task<SourceMaterial> CreateNewSource(SourceMaterial sourceMaterial)
        {
            try
            {
                return await _sourceMatRepository.AddSourceMaterial(sourceMaterial);
                
            }
            catch(Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
           
        }*/

/*i feel this method will be good when a device can only play certain media types or the app will be configured to support only some types of media files e.g high quality files only,so i can later add some other checks later like "if a file is of a particular extension type do this and that instead of going deep into the code and end up breaking in the middle ".
         */
        /*private string GetMediaType(string extension)
        {
            var result = MediaTypes.GetValueOrDefault(extension);
            if (result == null) { throw new ServiceException("Invalid file extension"); }
            else { return result; }
        }*/

        private async Task<int> AddFileDetails(ICollection<IFormFile> rawFiles,int Id,bool? forSource=true) 
        {
            var result = 0;
            if (rawFiles!= null && rawFiles.Count > 0)
            {
                var prefix = Directory.GetCurrentDirectory();
                var sourceFiles = new List<SourceFileDetail>();
                foreach (var file in rawFiles)
                {
                    var suffix = Path.Combine(prefix,file.ContentType) ;
                    var basePath = Path.Combine(prefix, suffix + "/" + file.FileName);

                    var sourceFile = new SourceFileDetail();
                    
                    sourceFile.FileType = file.ContentType;
                    Directory.CreateDirectory(suffix);
                    using (var stream = File.Create(basePath))
                    {
                        
                        await file.CopyToAsync(stream);

                    }
                    sourceFile.Path = basePath;
                    if (forSource == true)
                    {
                        sourceFile.SourceMaterialId = Id;
                    }
                    else
                    
                        sourceFiles.Add(sourceFile);
                    

                }
                result=await _sourceMatRepository.AddFileDeets(sourceFiles);
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

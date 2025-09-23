using Boompa.DTO;
using Boompa.Entities;
using Boompa.Exceptions;
using Boompa.Interfaces.IRepository;
using Boompa.Interfaces.IService;
using Elfie.Serialization;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Boompa.Implementations.Services
{
    public class SourceMaterialService : ISourceMaterialService
    {
        private readonly ISourceMaterialRepository _sourceMaterialRepository;
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
            _sourceMaterialRepository = sourceMatRepository;    /*********/
        }
        
        public async Task<Response> AddSourceMaterial(MaterialDTO.ArticleModel material)//, ICollection<MaterialDTO.QuestionModel> queModel
        {
            var result = new Response.ProgressResponse();
            var response = new Response();
            //the source material object is created
            var sourceMaterial = new SourceMaterial();
            if (material!= null)
            {
                sourceMaterial.Name = material.SourceMaterialName;
                sourceMaterial.Description = material.Description;
                sourceMaterial.Category = material.Category;
                sourceMaterial.Content = material.Text;
                sourceMaterial.CreatedBy = material.Creator; 
                
            }
            else
            {
                throw new ServiceException("No source material was provided");
            }
            if (material.RawFiles == null)
            {
                throw new ServiceException("sourceFiles not received");
            }

            var sourceMaterialId = await _sourceMaterialRepository.AddSourceMaterial(sourceMaterial);//the source material id is expected here 
            if (sourceMaterialId <= 0) { throw new ServiceException("Failed to add source material to database"); }
            else {response.StatusMessages.Add("Source material added successfully"); }

            var fileResult = await AddLocalFileDetails(material.RawFiles, sourceMaterialId);
            if(fileResult <= 0) { response.StatusMessages.Add("A problem occurred while saving files to device"); }
            else { response.StatusMessages.Add("Files saved successfully"); }
            result.Id = sourceMaterialId;
            

                return response;
        }

        public Task<Response> DeleteSourceMaterial()
        {





            throw new NotImplementedException();
        }

        public Task<Response> UpdateQuestion(string model)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateSourceMaterial(MaterialDTO rawMaterial)
        {
            
            throw new NotImplementedException();
        }

        /// <summary>
        ///to be able to identify which file belongs to which question i'll change the name of the file to a key that will be unique to the question.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="queFiles"></param>
        /// <param name="sourceMaterialId"></param>
        /// <returns></returns>
        /// <exception cref="ServiceException"></exception>
        public async Task<Response> AddQuestion(MaterialDTO.QuestionModel question,int sourceMaterialId)
        {
            try
            {
                var result = 0;
                var response = new Response();
                var que = new Question()
                {
                    Description = question.Description,
                    Answer = question.Answer,
                    SourceMaterialId = sourceMaterialId
                };

                var questionId = await _sourceMaterialRepository.AddQuestionAsync(que);//this question id will be passed to the add option method i'll create sometime later
                
                if (question.QueFiles != null)
                {
                    var fileResult = await AddLocalFileDetails(question.QueFiles, questionId, false);
                    if (fileResult <= 0) { response.StatusMessages.Add("A problem occurred while saving files to device"); }
                }
                //foreach (var question in model)
                //{

                //}
                return response;

            }catch(Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        //Description:  Responsible for adding any media file to the local file system and sends the file's information to the repository
        //
        //Parameters: files sent from the front end, id(either for question or sourcematerial, forSource-used to detect whether the id is for question or sourcematerial)
        //
        //
        //returns: 
        private async Task<int> AddLocalFileDetails(ICollection<IFormFile> rawFiles,int Id,bool? forSource=true) 
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
                        var ft = file.ContentType.Split('/')[0];
                        var suffix = Path.Combine(prefix, ft);
                        var basePath = Path.Combine(prefix, suffix + "/" + file.FileName);
                        Directory.CreateDirectory(suffix);
                        //File.Move(basePath, $"{file.Name}");
                        /*using (var stream = File.Create(basePath))
                        {
                              file.CopyToAsync(stream);
                        }*/
                var queFile = new QuestionFileDetail()
                        {
                            QuestionId = Id,
                            FileType = file.ContentType,
                            Path = basePath,
                        };
                        queFiles.Add(queFile);
                    }
                    result = await _sourceMaterialRepository.AddFileDetail(queFiles);
                }
                else
                {
                    var sourceFiles = new List<SourceFileDetail>();
                    foreach (var file in rawFiles)
                    {
                        var prefix = Directory.GetCurrentDirectory();
                        var suffix = Path.Combine(prefix, file.ContentType.Split('/')[0]);
                        var basePath = Path.Combine(prefix, suffix + "/" + file.FileName);
                        Directory.CreateDirectory(suffix);
                        //File.Move(file., $"{file.Name}");
                        /*using (var stream = File.Create(basePath))
                        {
                              file (stream);
                        }*/
                        var sourceFile = new SourceFileDetail()
                        {
                            SourceMaterialId = Id,
                            FileType = file.ContentType,
                            Path = basePath,
                        };
                        sourceFiles.Add(sourceFile);

                    }
                    result = await _sourceMaterialRepository.AddFileDetail(sourceFiles);

                }
            }

            
            return result;
        }

        public async Task<Response> AddCategory(string category)
        {
            throw new NotImplementedException();
        }

        

        public Task<Response> GetAllSourceMaterials()
        {
            //(ICollection < MaterialDTO.SourceDescriptor >) is the return type i am planning for this method
            throw new NotImplementedException();
        }

        public async Task<Response> GetSourceMaterial(string sourceMaterialName, string category)
        {
            var response = new Response();
            if (sourceMaterialName == null || category == null) { throw new ServiceException("no identifier received"); }
            response.Data = await _sourceMaterialRepository.GetSourceMaterial(sourceMaterialName,category);
            return response;
        }
    }
}

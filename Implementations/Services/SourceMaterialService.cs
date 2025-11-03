using Boompa.DTO;
using Boompa.Entities;
using Boompa.Exceptions;
using Boompa.Interfaces;
using Boompa.Interfaces.IRepository;
using Boompa.Interfaces.IService;
using Elfie.Serialization;
using System.Collections.Generic;
using System.Diagnostics;
//using System.IO;

namespace Boompa.Implementations.Services
{
    public class SourceMaterialService : ISourceMaterialService
    {
        private readonly ISourceMaterialRepository _sourceMaterialRepository;
        private readonly ICloudService _cloudService;
        private readonly IUnitOfWork _unitOfWork;
        

        public SourceMaterialService(ISourceMaterialRepository sourceMatRepository,BBb2StorageService storageService,IUnitOfWork unitOfWork)
        {
            _sourceMaterialRepository = sourceMatRepository; 
            _cloudService = storageService;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Response> AddSourceMaterial(MaterialDTO.ArticleModel material)
        {
            
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

            await _unitOfWork.SourceMaterials.AddSourceMaterial(sourceMaterial);
            await AddSourceFiles(material.RawFiles);
            //the source material id is expected here 
            //if (sourceMaterialId <= 0) { throw new ServiceException("Failed to add source material to database"); }
            //else {response.StatusMessages.Add("Source material added successfully"); }

            //var fileResult = await AddLocalFileDetails(material.RawFiles, sourceMaterialId);

            
            //if(fileResult <= 0) { response.StatusMessages.Add("A problem occurred while saving files to device"); }
            //else { response.StatusMessages.Add("Files saved successfully"); }
            //response.Data = sourceMaterialId;
            

                return response;
        }

        public async Task AddSourceFiles(ICollection<IFormFile> files)
        {
            try
            {
                foreach (var file in files)
                {
                    var cloudFile = new CloudSourceFileDetails()
                    {
                        Key = file.FileName,
                        FileType = file.ContentType
                    };
                    await _unitOfWork.SourceMaterials.AddCloudSourceFile(cloudFile);

                }

                await _cloudService.UploadFilesAsync(files);
            }
            catch(Exception ex)
            {
                throw new ServiceException($"{ex.Message},{ex.InnerException?.Message}");
            }
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
                    Options = question.Option,
                    SourceMaterialId = sourceMaterialId
                };

                await _unitOfWork.SourceMaterials.AddQuestionAsync(que);
                
                if (question.QueFiles != null)
                {
                     await AddEvalFiles(question.QueFiles);
                    
                }
                
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
        private async Task<int> AddLocalFileDetails(ICollection<IFormFile> rawFiles,int Id,bool forSource=true) 
        {
            var result = 0;
            if (rawFiles!= null && rawFiles.Count > 0)
            {
                if (forSource == false)
                {
                    var queFiles = new List<QuestionFileDetail>();
                    foreach (var file in rawFiles)
                    {
                        //await _cloudService.UploadFileAsync(file);
                        var prefix = Directory.GetCurrentDirectory();
                        var ft = file.ContentType.Split('/')[0];
                        var suffix = Path.Combine(prefix, ft);
                        var basePath = Path.Combine(prefix, suffix + "/" + file.FileName);
                        Directory.CreateDirectory(suffix);
                        
                        var queFile = new QuestionFileDetail()
                        {
                            QuestionId = Id,
                            FileType = file.ContentType,
                            Path = basePath,
                        };
                        queFiles.Add(queFile);
                    }
                     await _unitOfWork.SourceMaterials.AddFileDetail(queFiles);
                }
                else
                {
                    var sourceFiles = new List<SourceFileDetail>();
                    
                    foreach (var file in rawFiles)
                    {
                        //await _cloudService.UploadFileAsync (file);
                        var prefix = Directory.GetCurrentDirectory();
                        var suffix = Path.Combine(prefix, file.ContentType.Split('/')[0]);
                        var basePath = Path.Combine(prefix, suffix + "/" + file.FileName);
                        Directory.CreateDirectory(suffix);
                        
                        var sourceFile = new SourceFileDetail()
                        {
                            SourceMaterialId = Id,
                            FileType = file.ContentType,
                            Path = basePath,
                        };
                        sourceFiles.Add(sourceFile);

                    }
                    await _unitOfWork.SourceMaterials.AddFileDetail(sourceFiles);

                }
            }

            
            return result;
        }

        private async Task UploadFilesAsync(ICollection<IFormFile> files)
        {
            foreach(var file in files)
            {
                var key = file.FileName;


                await _cloudService.UploadFilesAsync(files);
            }

        }
        private async Task<ICollection<Stream>> GetFilesAsync(ICollection<string> keys)
        {
            try
            {

                var result = await _cloudService.GetFilesAsync(keys);
                return result;
            }
            catch(Exception ex)
            {
                throw new CloudException(ex.Message);
            }
           
            
        }


        public async Task<Response> AddCategory(string category)
        {
            throw new NotImplementedException();
        }

        

        public async Task<Response> GetAllSourceMaterials(string categoryName)
        {
            var response = new Response();  
            var result = _sourceMaterialRepository.GetAllSourceMaterials(categoryName);
            response.Data = result;
            response.StatusCode = 200;
            response.StatusMessages.Add("success");

            return response;
        }

        public async Task<Response> GetSourceMaterial(string sourceMaterialName, string category)
        {
            var response = new Response();
            if (sourceMaterialName == null || category == null) { throw new ServiceException("no identifier received"); }
            response.Data = await _sourceMaterialRepository.GetSourceMaterial(sourceMaterialName,category);
            return response;
        }
        public async Task<Response> GetSourceMaterial(string key)
        {
            var response = new Response();
            if (key == null) { throw new ServiceException("no identifier received"); }
            response.Data = await _cloudService.GetFileAsync(key);
            return response;
        }

        public async Task<Response> AddQuestion(IEnumerable<MaterialDTO.QuestionModel> models)
        {
            var response = new Response();
            var Questions = new List<Question>();
            try
            {
                foreach (var question in models)
                {
                    var que = new Question()
                    {
                        Description = question.Description,
                        Answer = question.Answer,
                        Options = question.Option,

                    };
                    //Questions.Add(que);
                     await _unitOfWork.SourceMaterials.AddQuestionAsync(que);
                    if(question.QueFiles != null)
                    {
                        await AddEvalFiles(question.QueFiles);
                    }
                    else
                    {
                        response.StatusMessages.Add("No files added for this question");
                    }

                    //if (question.QueFiles != null)
                    //{
                    //    var fileResult = await AddLocalFileDetails(question.QueFiles, questionId, false);
                    //    if (fileResult <= 0) { response.StatusMessages.Add("A problem occurred while saving files to device"); }
                    //}
                    
                   
                }
                    return response;
            }
            catch (Exception ex)
            {
                response.StatusMessages.Add(ex.Message);
                return response;
            }
        }

        private async Task AddEvalFiles(ICollection<IFormFile> evalFiles)
        {
            foreach (var file in evalFiles)
            {
                var cloudFile = new CloudEvalFileDetails()
                {
                    Key = file.FileName,
                    FileType = file.ContentType
                };
                await _unitOfWork.SourceMaterials.AddCloudEvalFile(cloudFile);

            }

            await _cloudService.UploadFilesAsync(evalFiles);
        }
    }
}

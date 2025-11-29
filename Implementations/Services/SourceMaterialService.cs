using Boompa.DTO;
using Boompa.Entities;
using Boompa.Exceptions;
using Boompa.Interfaces;
using Boompa.Interfaces.IRepository;
using Boompa.Interfaces.IService;

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
                sourceMaterial.Content = material.Text;
                sourceMaterial.CreatedBy = material.Creator;
                sourceMaterial.CreatedOn = material.CreatedOn;
            }
            else
            {
                throw new ServiceException("No source material was provided");
            }

            var category = await GetCategory(material.Category);
            if (category != null)
            {
                sourceMaterial.CategoryId = category.Id;
            }
            else
            {
                throw new ServiceException("the category specified does not exist");
            }

            if (material.RawFiles.Count != 0)
            {
                await AddSourceFiles(material.RawFiles!, sourceMaterial);
            }
            else
            {
                response.StatusMessages.Add("no files were received");
            }
            var source = await _unitOfWork.SourceMaterials.AddSourceMaterial(sourceMaterial);
            var result = await _unitOfWork.SaveChangesAsync();

            if(result > 0)
            {
                response.StatusMessages.Add("success");
                response.Data = sourceMaterial.Id;
            }
            return response;
        }

        private async Task<SourceMaterial> AddSourceFiles(ICollection<IFormFile> files, SourceMaterial sourceMaterial)
        {
            try
            {
                foreach (var file in files)
                {
                    var prefix = Guid.NewGuid().ToString();
                    var key  = prefix.Concat($"|{file.FileName}").ToString();
                    sourceMaterial.Files.Add(key);
                }
                
                await _cloudService.UploadFilesAsync(files);
                return sourceMaterial;
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

        
        public async Task<Response> AddQuestion(MaterialDTO.QuestionModel question,Guid sourceMaterialId)
        {
            try
            {
                var response = new Response();
                var que = new Question()
                {
                    SourceMaterialId = sourceMaterialId,
                    Description = question.Description,
                    Answer = question.Answer,
                    Options = question.Option,
                    
                };

                if (question.QueFiles.Count != 0)
                {
                    foreach(var file in question.QueFiles)
                    {
                        var prefix = Guid.NewGuid().ToString();
                        var key = prefix.Concat($"|{file.FileName}").ToString();
                        que.Files.Add(key);
                    }

                }

                var ques = await _unitOfWork.SourceMaterials.AddQuestionAsync(que);
                var result = await _unitOfWork.SaveChangesAsync();

                if (result > 0)
                {
                    response.StatusMessages.Add("success");
                    
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
        //private async Task<int> AddLocalFileDetails(ICollection<IFormFile> rawFiles,int Id,bool forSource=true) 
        //{
        //    var result = 0;
        //    if (rawFiles!= null && rawFiles.Count > 0)
        //    {
        //        if (forSource == false)
        //        {
        //            var queFiles = new List<QuestionFileDetail>();
        //            foreach (var file in rawFiles)
        //            {
        //                //await _cloudService.UploadFileAsync(file);
        //                var prefix = Directory.GetCurrentDirectory();
        //                var ft = file.ContentType.Split('/')[0];
        //                var suffix = Path.Combine(prefix, ft);
        //                var basePath = Path.Combine(prefix, suffix + "/" + file.FileName);
        //                Directory.CreateDirectory(suffix);
                        
        //                var queFile = new QuestionFileDetail()
        //                {
        //                    QuestionId = Id,
        //                    FileType = file.ContentType,
        //                    Path = basePath,
        //                };
        //                queFiles.Add(queFile);
        //            }
        //             await _unitOfWork.SourceMaterials.AddFileDetail(queFiles);
        //        }
        //        else
        //        {
        //            var sourceFiles = new List<SourceFileDetail>();
                    
        //            foreach (var file in rawFiles)
        //            {
        //                //await _cloudService.UploadFileAsync (file);
        //                var prefix = Directory.GetCurrentDirectory();
        //                var suffix = Path.Combine(prefix, file.ContentType.Split('/')[0]);
        //                var basePath = Path.Combine(prefix, suffix + "/" + file.FileName);
        //                Directory.CreateDirectory(suffix);
                        
        //                var sourceFile = new SourceFileDetail()
        //                {
        //                    SourceMaterialId = Id,
        //                    FileType = file.ContentType,
        //                    Path = basePath,
        //                };
        //                sourceFiles.Add(sourceFile);

        //            }
        //            await _unitOfWork.SourceMaterials.AddFileDetail(sourceFiles);

        //        }
        //    }

            
        //    return result;
        //}

        
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


        public async Task<Response> AddCategory(string categoryName)
        {
            var response = new Response();
            var category = new Category
            {
                Name = categoryName
            };
            await _unitOfWork.SourceMaterials.AddCategory(category);
            var result = await _unitOfWork.SaveChangesAsync();
            if(result > 0)
            {
                response.StatusMessages.Add("success");
                response.Data = result;
            }
            return response;
        }

        

        public async Task<Response> GetAllSourceMaterials(string categoryName)
        {
            var response = new Response();  
            var result = await _sourceMaterialRepository.GetAllSourceMaterials(categoryName);
            if(result == null) { throw new ServiceException("no materials for this category yet"); }
            response.Data = result;
            response.StatusCode = 200;
            response.StatusMessages.Add("success");

            return response;
        }

        public async Task<Response> GetSourceMaterial(string sourceMaterialName, string category)
        {
            //get the source material from the database
            var response = new Response();
            if (sourceMaterialName == null || category == null) { throw new ServiceException("no identifier received"); }
            var result = await _sourceMaterialRepository.GetSourceMaterial(sourceMaterialName,category);

            //check if deleted
            if(result.IsDeleted == true)
            {
                response.StatusMessages.Add("source not available");
                return response;
            }
           
            var model = new MaterialDTO.ConsumptionModel()
            {
                
                MaterialName = result.Name,
                CategoryId = result.Category.Id,
                Content = result.Content,
                Questions = result.Questions.Select(a => new MaterialDTO.QuestionDto
                {
                    Question = a.Description,
                    Answer = a.Answer,
                    Options = a.Options,
                }).ToList(),
                //SourceFiles = this will be implemented in due time
            };

            response.StatusCode = 200;
            response.StatusMessages.Add("success");
            response.Data = model;
            
            return response;
        }
        public async Task<Response> GetSourceFile(string key)
        {
            var response = new Response();
            if (key == null) { throw new ServiceException("no identifier received"); }
            response.Data = await _cloudService.GetFileAsync(key);
            return response;
        }

        public async Task<Response> GetSourceMaterial(string category, Guid sourceId)
        {
            var response = new Response();
            var result = await _unitOfWork.SourceMaterials.GetSourceMaterial(category, sourceId);
            if (result == null) { throw new ServiceException("source material not found");}

            var model = new MaterialDTO.ConsumptionModel()
            {
                MaterialName = result.Name,
                CategoryId = result.CategoryId,
                Content = result.Content,
                Questions = result.Questions.Select(a => new MaterialDTO.QuestionDto
                {
                    Question = a.Description,
                    Answer = a.Answer,
                    Options = a.Options,
                }).ToList(),
                //SourceFiles = this will be implemented in due time
            };

            response.StatusCode = 200;
            response.StatusMessages.Add("success");
            response.Data = model;

            return response;
        }
        public async Task<Response> AddQuestion(ICollection<MaterialDTO.QuestionModel> models,string sourceMaterialName,string category)
        {
            var response = new Response();
             
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

                    //var qus = await _unitOfWork.SourceMaterials.AddQuestionAsync(que, sourceMaterialName, category);
                    if (question.QueFiles.Count != 0)
                    {
                        var qus = await AddEvalFiles(question.QueFiles, que);
                        await _unitOfWork.SourceMaterials.AddQuestionAsync(qus, sourceMaterialName, category);
                    }
                    else
                    {
                        response.StatusMessages.Add("No files added for this question");
                    }
                }

                var result = await _unitOfWork.SaveChangesAsync();
                if(result >= 1)
                {
                    response.StatusMessages.Add("success");
                    response.Data = result;
                    return response;
                }
                else
                {
                    response.StatusCode = 500;
                    response.StatusMessages.Add("one or more problems occurred");
                    return response;
                }

                    

            }
            catch (Exception ex)
            {
                response.StatusMessages.Add(ex.Message);
                return response;
            }
        }

        public async Task<Response> AddQuestion(ICollection<MaterialDTO.QuestionModel> models,Guid sourceMaterialId)
        {
            var response = new Response();

            try
            {
                foreach (var question in models)
                {
                    var que = new Question()
                    {
                        SourceMaterialId = sourceMaterialId,
                        Description = question.Description,
                        Answer = question.Answer,
                        Options = question.Option,

                    };

                    
                    if (question.QueFiles.Count != 0)
                    {
                        var qus = await AddEvalFiles(question.QueFiles, que);
                        //await _unitOfWork.SourceMaterials.AddQuestionAsync(qus);
                    }
                    else
                    {
                        response.StatusMessages.Add("No files added for this question");
                    }
                }

                var result = await _unitOfWork.SaveChangesAsync();
                if (result >= 1)
                {
                    response.StatusMessages.Add("success");
                    response.Data = result;
                    return response;
                }
                else
                {
                    response.StatusCode = 500;
                    response.StatusMessages.Add("one or more problems occurred");
                    return response;
                }



            }
            catch (Exception ex)
            {
                response.StatusMessages.Add(ex.Message);
                return response;
            }
        }

        private async Task<Question> AddEvalFiles(ICollection<IFormFile> evalFiles,Question question)
        {
            foreach (var file in evalFiles)
            {
               var prefix =  Guid.NewGuid().ToString();
                var key = prefix.Concat($"|{file.FileName}").ToString();
                question.Files.Add(key);

            }
            await _cloudService.UploadFilesAsync(evalFiles);
            return question;
        }

        private async Task<bool> CategoryExists(string categoryName)
        {
            return await _unitOfWork.SourceMaterials.CategoryExists(categoryName);
        }
        private async Task<Category> GetCategory(string categoryName)
        {
            return await _unitOfWork.SourceMaterials.GetCategoryId(categoryName);
        }

       
    }
}

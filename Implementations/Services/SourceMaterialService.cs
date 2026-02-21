using Boompa.DTO;
using Boompa.Entities;
using Boompa.Exceptions;
using Boompa.Implementations.Repositories;
using Boompa.Interfaces;
using Boompa.Interfaces.IRepository;
using Boompa.Interfaces.IService;
using System.Data;



namespace Boompa.Implementations.Services
{
    public class SourceMaterialService(ICloudService storageService, IUnitOfWork unitOfWork, ISourceMaterialRepository sourceMaterialRepository) : ISourceMaterialService
    {
        private readonly ICloudService _cloudService = storageService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ISourceMaterialRepository _sourceMaterialRepository = sourceMaterialRepository;

       
        public async Task<Response> AddSourceMaterial(MaterialDTO.ArticleModel material, string creator)
        {

            var response = new Response();
            //the source material object is created
            var sourceMaterial = new SourceMaterial();

            if (material != null)
            {
                sourceMaterial.Name = material.SourceMaterialName;
                sourceMaterial.Description = material.Description;
                sourceMaterial.Content = material.TextContent;
                sourceMaterial.CreatedBy = creator;
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

            if (result > 0)
            {
                response.StatusMessages.Add("success");
                response.Data = sourceMaterial.Id;
            }
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

        
        public async Task<Response> AddQuestion(MaterialDTO.QuestionModel question,Guid sourceMaterialId)
        {
            try
            {

                var que = new Question();
                switch (question.QuestionType)
                {
                    // type1 question is for pure mcq questions where the question, its answer, and options are text/string
                    case "type1":
                        que.SourceMaterialId = sourceMaterialId;
                        que.Description = question.TextDescription;
                        que.Answer = question.Answer;
                        que.Options = question.Option;
                        break;
                        //type2 questions are for questions with images as their questions and their answers and options as text/string
                    case "type2":
                        var file = question.FileDescription;
                        var prefix = Guid.NewGuid().ToString();
                        var key = $"{prefix}/{file.FileName}";
                        que.Files.Add(key);
                        await _cloudService.UploadFileAsync(file, key);

                        que.Answer = question.Answer;
                        que.Options = question.Option;

                        break;
                }
                
                var response = new Response();
                //save data to database
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
            var result = await _unitOfWork.SourceMaterials.GetAllSourceMaterials(categoryName);
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
            var result = await _unitOfWork.SourceMaterials.GetSourceMaterial(sourceMaterialName,category);

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
                TextContent = result.Content,
                Questions = result.Questions.Select(a => new MaterialDTO.QuestionDto
                {
                    TextQuestion = a.Description,
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
            response.Data = await _cloudService.GetFileUrlAsync(key);
            return response;
        }

        public async Task<Response> GetSourceMaterial(string category, Guid sourceId)
        {
            var response = new Response();
            //get the source materaial from the database
            var result = await _unitOfWork.SourceMaterials.GetSourceMaterial(category, sourceId);
            if (result == null) { throw new ServiceException("source material not found");}

            //mapping to the return entity
            var model = new MaterialDTO.ConsumptionModel()
            {
                MaterialName = result.Name,
                CategoryId = result.CategoryId,
                TextContent = result.Content,
            };

            //mapping the file URLs
            foreach (var file in result.Files) 
            {
                var fileUrl = await GetFileAsync(file);

                var dto = new FileDTO.ReturnDTO();
                dto.FileURL = fileUrl;
                dto.Index = file.Split("|")[1];

                model.SourceFiles.Add(dto);
            }

            //mapping the questions
            foreach (var question in result.Questions)
            {
                switch (question.QuestionType)
                {
                    case "default":
                        var queResponse = new MaterialDTO.QuestionDto
                        {
                            TextQuestion = question.Description,
                            Answer = question.Answer,
                            Options = question.Options,
                            QuestionType = question.QuestionType,
                        };
                        model.Questions.Add(queResponse);


                        break;

                    case "type2":
                        var file = await GetFileAsync(question.Description);
                       var queResponse2 = new MaterialDTO.QuestionDto
                        {
                            FileQuestion = file,
                            Answer = question.Answer,
                            Options = question.Options,
                            QuestionType = question.QuestionType,
                        };
                        model.Questions.Add(queResponse2); 
                        break;
                }   
               
            }
            
            //returning the result 
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
                var que = new Question();
                foreach (var question in models)
                {

                    switch (question.QuestionType)
                    {
                        // type1 question is for pure mcq questions where the question, its answer, and options are text/string
                        case "type1":
                            
                            que.Description = question.TextDescription;
                            que.Answer = question.Answer;
                            que.Options = question.Option;
                            break;
                        //type2 questions are for questions with images as their questions and their answers and options as text/string
                        case "type2":
                            var file = question.FileDescription;
                            var prefix = Guid.NewGuid().ToString();
                            var key = $"{prefix}|{file.FileName}";
                            que.Files.Add(key);
                            await _cloudService.UploadFileAsync(file, key);

                            que.Answer = question.Answer;
                            que.Options = question.Option;
                            break;
                    }

                }
                await _unitOfWork.SourceMaterials.AddQuestionAsync(que, sourceMaterialName, category);
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
                    var que = new Question();
                    switch (question.QuestionType)
                    {
                        // type1 question is for pure mcq questions where the question, its answer, and options are text/string
                        case "default":
                            que.SourceMaterialId = sourceMaterialId;
                            que.Description = question.TextDescription;
                            que.Answer = question.Answer;
                            que.Options = question.Option;
                            que.QuestionType = question.QuestionType;

                            await _unitOfWork.SourceMaterials.AddQuestionAsync(que);
                            break;
                        //type2 questions are for questions with images as their questions and their answers and options as text/string
                        case "type2":
                            
                            var file = question.FileDescription;

                            var prefix = Guid.NewGuid().ToString();
                            var key = $"{prefix}|{file.FileName}";
                            
                            await _cloudService.UploadFileAsync(file,key);

                            que.Description = key;
                            que.SourceMaterialId = sourceMaterialId;
                            que.Answer = question.Answer;
                            que.Options = question.Option;
                            que.QuestionType = question.QuestionType;

                            await _unitOfWork.SourceMaterials.AddQuestionAsync(que);
                            break;
                    }

                }
                var result = await _unitOfWork.SaveChangesAsync();
                if (result >= 1)
                {
                    response.StatusCode = 200;
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
       
        private async Task<Category> GetCategory(string categoryName)
        {
            var res = await _sourceMaterialRepository.GetCategoryId(categoryName);
            return res;
        }

        private async Task<SourceMaterial> AddSourceFiles(ICollection<FileDTO> receivedFiles, SourceMaterial sourceMaterial)
        {
            try
            {
                var files = new Dictionary<string,IFormFile>();
                foreach (var fileDetails in receivedFiles)
                {
                    var prefix = Guid.NewGuid().ToString();
                    var key = $"{prefix}|{fileDetails.Index}|{fileDetails.File.FileName}";
                    sourceMaterial.Files.Add(key);

                    files.Add(key,fileDetails.File);
                }

                await _cloudService.UploadFilesAsync(files);
                return sourceMaterial;
            }
            catch (Exception ex)
            {
                throw new ServiceException($"{ex.Message},{ex.InnerException?.Message}");
            }
        }
        private async Task<string> GetFileAsync(string key)
        {
            try
            {
                var result = await _cloudService.GetFileUrlAsync(key);
                return result;
            }
            catch (Exception ex)
            {
                throw new CloudException(ex.Message);
            }


        }

        private async Task<ICollection<Stream>> GetFilesAsync(ICollection<string> keys)
        {
            try
            {

                var result = await _cloudService.GetFilesAsync(keys);
                return result;
            }
            catch (Exception ex)
            {
                throw new CloudException(ex.Message);
            }


        }

        private async Task<string> AddSourceFile(FileDTO file)
        {
            var prefix = Guid.NewGuid().ToString();
            var key = prefix.Concat($"|{file.Index}|{file.File.FileName}").ToString();

            await _cloudService.UploadFileAsync(file.File, key);
            return key;
        }

        public async Task<Response> AddSourceMaterial(MaterialDTO.TinyModel model)
        {
            var response = new Response();
            try
            {
                if(model == null)
                {
                    response.StatusCode = 400;
                    response.StatusMessages.Add("No data received");
                    return response;
                }

                var category = await GetCategory(model.Category);
                var material = new SourceMaterial()
                {
                    Name = model.SourceMaterialName,
                    Description = model.Description,
                    CategoryId = category.Id,
                    Content = model.Content,
                    CreatedOn = model.CreatedOn,
                    CreatedBy = "warriman"
                };

                await _unitOfWork.SourceMaterials.AddSourceMaterial(material);
                var result = await _unitOfWork.SaveChangesAsync();

               if(result > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessages.Add("success");
                    response.Data = material.Id;
                }
            

                
                return response;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
               
            }
        }
    }
}

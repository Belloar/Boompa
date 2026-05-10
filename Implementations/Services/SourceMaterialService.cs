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
            if (material.Categories != null)
            {
                foreach (var category in material.Categories)
                {
                    var existingCategory = await GetCategory(category);
                    if (existingCategory != null)
                    {
                        var csm = new CategorySourceMaterial
                        {
                            Category = existingCategory,
                            SourceMaterial = sourceMaterial
                        };
                        sourceMaterial.Categories.Add(csm);
                    }
                    else
                    {
                        response.StatusMessages.Add($"category {category} does not exist");
                    }
                }
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
                que.SourceMaterialId = sourceMaterialId;
                que.Description = question.TextDescription;
                que.Answer = question.Answer;
                que.Options = question.Option;
                que.QuestionType = question.QuestionType;

                //switch (question.QuestionType)
                //{
                //    // type1 question is for pure mcq questions where the question, its answer, and options are text/string
                //    case "mcq":
                //        que.SourceMaterialId = sourceMaterialId;
                //        que.Description = question.TextDescription;
                //        que.Answer = question.Answer;
                //        que.Options = question.Option;
                //        break;
                //    //type2 questions are for questions with images as their questions and their answers and options as text/string
                //    case "type2":
                //        var file = question.FileDescription;
                //        var prefix = Guid.NewGuid().ToString();
                //        var key = $"{prefix}/{file.FileName}";
                //        que.Files.Add(key);
                //        await _cloudService.UploadFileAsync(file, key);

                //        que.Answer = question.Answer;
                //        que.Options = question.Option;

                //        break;
                //}

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

        public async Task<Response> GetAllSourceMaterials(int pageSize)
        {
            var response = new Response();
            try
            {

                
                var result = await _unitOfWork.SourceMaterials.GetAll(pageSize);
                if (result == null)
                {
                    response.StatusCode = 500;
                    response.StatusMessages.Add("No materials within this category");
                    return response;
                }

                response.Data = result;
                response.StatusCode = 200;
                response.StatusMessages.Add("success");

                return response;
            }
            catch (Exception ex) 
            { 
                response.StatusCode = 500;
                response.StatusMessages.Add($"{ex.Message}");
                response.Data = ex;
                return response;
            }
        }

        public async Task<Response> GetAllSourceMaterials(Guid categoryId, int pageNumber)
        {
            var response = new Response();
            try
            {
                var result = await _unitOfWork.SourceMaterials.GetAll(categoryId, pageNumber);
                if (result == null)
                {
                    response.StatusCode = 500;
                    response.StatusMessages.Add("No materials within this category");
                    return response;
                }

                response.Data = result;
                response.StatusCode = 200;
                response.StatusMessages.Add("success");

                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessages.Add($"{ex.Message}");
                response.Data = ex;
                return response;
            }
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
                TextContent = result.Content,
                Categories = result.Categories.Select( cat => new MaterialDTO.CategoryDTO
                {
                    Id = cat.CategoryId,
                    Name = cat.Category.Name,
                }).ToList(),
                Questions = result.Questions.Select(a => new MaterialDTO.QuestionDTO
                {
                    TextQuestion = a.Description,
                    Answer = a.Answer,
                    Options = a.Options,
                    QuestionType = a.QuestionType,
                }).ToList(),
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

        public async Task<Response> GetSourceMaterial(Guid sourceId)
        {
            var response = new Response();
            //get the source materaial from the database
            var result = await _unitOfWork.SourceMaterials.GetSourceMaterial(sourceId);
            if (result == null)
            {
                response.StatusMessages.Add("Source material does not exist");
                return response;
            }

            //mapping to the return entity
            var model = new MaterialDTO.ConsumptionModel()
            {
                SourceId = result.Id,
                MaterialName = result.Name,
                TextContent = result.Content,

                Categories = result.Categories.Select(cat => new MaterialDTO.CategoryDTO
                {
                    Id = cat.CategoryId,
                    Name = cat.Category.Name,
                }).ToList(),

                Questions = result.Questions.Select(que => new MaterialDTO.QuestionDTO
                {
                    TextQuestion = que.Description,
                    Answer = que.Answer,
                    Options = que.Options,
                    QuestionType = que.QuestionType,
                }).ToList()
            };

            //mapping the questions
            //foreach (var question in result.Questions)
            //{
            //    switch (question.QuestionType)
            //    {
            //        case "default":
            //            var queResponse = new MaterialDTO.QuestionDTO
            //            {
            //                TextQuestion = question.Description,
            //                Answer = question.Answer,
            //                Options = question.Options,
            //                QuestionType = question.QuestionType,
            //            };
            //            model.Questions.Add(queResponse);


            //            break;

            //        case "type2":
            //            var file = await GetFileAsync(question.Description);
            //           var queResponse2 = new MaterialDTO.QuestionDTO
            //            {
            //                FileQuestion = file,
            //                Answer = question.Answer,
            //                Options = question.Options,
            //                QuestionType = question.QuestionType,
            //            };
            //            model.Questions.Add(queResponse2); 
            //            break;
            //    }   
               
            //}
            
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
                        
                    }

                }
                //await _unitOfWork.SourceMaterials.AddQuestionAsync(que, sourceMaterialName, category);
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
                    que.SourceMaterialId = sourceMaterialId;
                    que.Description = question.TextDescription;
                    que.Answer = question.Answer;
                    que.Options = question.Option;
                    que.QuestionType = question.QuestionType;

                    await _unitOfWork.SourceMaterials.AddQuestionAsync(que);
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
                


                var material = new SourceMaterial()
                {
                    Name = model.SourceMaterialName,
                    Description = model.Description,
                    Content = model.Content,
                    CreatedOn = model.CreatedOn,
                    CreatedBy = model.CreatedBy
                    //ADDRESS THE CREATOR FIELD SOMETIME
                };

                foreach (var category in model.Categories)
                {
                    var existingCategory = await _unitOfWork.SourceMaterials.GetCategory(category);
                    var cat = new CategorySourceMaterial
                    {
                        Category = existingCategory,
                        SourceMaterial = material
                    };
                    material.Categories.Add(cat);
                }

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

        public async Task<Response> GetRandomSource()
        {
            var response = new Response();
            var sourceMaterial = await _unitOfWork.SourceMaterials.GetRandomSource();
            var result = new MaterialDTO.SourceDescriptor()
            {
                SourceId = sourceMaterial.Id,
                SourceDescription = sourceMaterial.Description,
                SourceName = sourceMaterial.Name,
                Categories = sourceMaterial.Categories.Select(cat => new MaterialDTO.CategoryDetails
                {
                    CategoryId = cat.Id,
                    Name = cat.Category.Name,
                }).ToList()
            };
            response.StatusCode = 200;
            response.StatusMessages.Add("success");
            response.Data = result;

            return response;
        }

        public async Task<Response> GetTopCategories(string learnerId)
        {
            var response = new Response();  
            if(learnerId != default)
            {
                var result = await _unitOfWork.SourceMaterials.GetTopCategories(learnerId);
                response.StatusCode = 200;
                response.StatusMessages.Add("success");
                response.Data = result;
                return response;
            }
            else
            {
                response.StatusCode = 400;
                response.StatusMessages.Add("An error occured while fetching resource");
                return response;
            }
            
        }

        public async Task<Response> GetCategories()
        {
            var response = new Response();

            try
            {
                var result = await _unitOfWork.SourceMaterials.GetCategories();
                response.StatusCode = 200;
                response.StatusMessages.Add("success");
                response.Data = result;

                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessages.Add($"{ex.Message}");
                response.Data = ex;

                return response;
            }
        }

        
    }
}

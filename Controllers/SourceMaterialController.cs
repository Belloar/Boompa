using Boompa.DTO;
using Boompa.Entities;
using Boompa.Implementations.Services;
using Boompa.Implementations.Services.AppServices;
using Boompa.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace Boompa.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SourceMaterialController(
        ISourceMaterialService sourceMaterialService,
        IAIQuestionGenerator questionGenerator,
        IQuestionService questionService) : ControllerBase
    {

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSourceMaterial([FromForm] MaterialDTO.ArticleModel sourceMaterial)
        {
            try
            {
                var creator = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (sourceMaterial == null) { return BadRequest("Material not received by server"); }

                if (creator != null)
                {
                    var result = await sourceMaterialService.AddSourceMaterial(sourceMaterial, creator);
                    return Ok(result);
                }
                else
                {
                    var result = await sourceMaterialService.AddSourceMaterial(sourceMaterial);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNewSourceMaterial([FromForm] MaterialDTO.TinyModel model)
        {
            var result = await sourceMaterialService.AddSourceMaterial(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] string category)
        {
            if (category == null) { return BadRequest("Material not received"); }
            try
            {
                var result = await sourceMaterialService.AddCategory(category);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GenerateQuestion([FromBody] MaterialDTO.AIQueGenDTO model)
        {
            var result = questionGenerator.GenerateQuestions(model.Material, model.Prompt);
            return Ok(result);
        }

        [HttpPost("questionType")]
        public async Task<IActionResult> AddQuestion(
            [FromForm] MaterialDTO.QuestionModel model,
            [FromQuery] string questionType)
        {
            if (model == null) { return BadRequest("Material not received"); }
            try
            {
                var questionTypeEnum = ResolveQuestionType(questionType);
                await questionService.AddQuestion(model, questionTypeEnum);
                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{sourceId}")]
        public async Task<IActionResult> GetSourceMaterial(Guid sourceId)
        {
            if (sourceId == Guid.Empty) { return BadRequest("SourceMaterialName not provided"); }
            try
            {
                var result = await sourceMaterialService.GetSourceMaterial(sourceId);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }


        [HttpGet("{pageNumber}")]
        public async Task<IActionResult> GetSourceMaterialNames(int pageNumber)
        {
            var result = await sourceMaterialService.GetAllSourceMaterials(pageNumber);
            return Ok(result);

        }

        [HttpGet("{categoryId}/{pageNumber}")]
        public async Task<IActionResult> GetSourceMaterialNames(int pageNumber, Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                return BadRequest("something went wrong");

            }
            var result = await sourceMaterialService.GetAllSourceMaterials(categoryId, pageNumber);
            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> AddQuestionsByName([FromForm] ICollection<MaterialDTO.QuestionModel> questions, [FromHeader] string sourceMaterialName, [FromHeader] string category)
        {
            //await questionService.AddQuestion(questions, sourceMaterialName, category);
            //return Created();
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestionsByGuid([FromBody] ICollection<MaterialDTO.QuestionModel> questions, [FromHeader] Guid sourceId)
        {
            //if (questions == null) { return BadRequest("Payload not received"); }
            //var result = await sourceMaterialService.AddQuestion(questions, sourceId);
            //return Ok(result);

            throw new NotImplementedException();
        }

        [HttpGet]
        [Authorize(Roles = "Learner")]
        public async Task<IActionResult> GetTopCategories()
        {
            var learnerId = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (learnerId == null) { return StatusCode(500, "invalid credentials"); }

            var result = await sourceMaterialService.GetTopCategories(learnerId);
            return Ok(result);
        }

        [HttpGet]
        //[Authorize(Roles = "Learner")]
        public async Task<IActionResult> GetRandomSource()
        {
            var result = await sourceMaterialService.GetRandomSource();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var result = await sourceMaterialService.GetCategories();
            return Ok(result);
        }

        private QuestionType ResolveQuestionType(string type)
        {
            return type.Trim().ToLower() switch
            {
                "freetext" => QuestionType.FreeText,
                "multiplechoice" => QuestionType.MultipleChoice,
                "multipleresponse" => QuestionType.MultipleResponse,
                _ => throw new ArgumentException($"Invalid question type: {type}")
            };


        }
    }

    
}

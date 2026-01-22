using Boompa.DTO;
using Boompa.Entities;
using Boompa.Implementations.Services;
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
    public class SourceMaterialController : ControllerBase
    {
        private readonly ISourceMaterialService _sourceMaterialService;
        public SourceMaterialController(ISourceMaterialService sourceMaterialService) 
        {
            _sourceMaterialService = sourceMaterialService;            
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSourceMaterial([FromForm] MaterialDTO.ArticleModel sourceMaterial)
        {
            try
            {
                var creator = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (sourceMaterial == null) { return BadRequest("Material not received by server"); }

                if(creator != null)
                {
                    var result = await _sourceMaterialService.AddSourceMaterial(sourceMaterial, creator);
                    return Ok(result);
                }
                else
                {
                    var result = await _sourceMaterialService.AddSourceMaterial(sourceMaterial);
                    return Ok(result);
                }

               


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] string category)
        {
            if(category == null) { return BadRequest("Material not received"); }
            try
            {
                var result = await _sourceMaterialService.AddCategory(category);
                return Ok(result);

            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion([FromForm]MaterialDTO.QuestionModel model, [FromHeader] Guid sourceMaterialId)
        {
            if (model == null) { return BadRequest("Material not received"); }
            try
            {
                var result = await _sourceMaterialService.AddQuestion(model,sourceMaterialId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSourceMaterial([FromHeader]Guid sourceId, [FromHeader]string category)
        {
            if (sourceId == null) { return BadRequest("SourceMaterialName not provided"); }
            try
            {
                var result  = await _sourceMaterialService.GetSourceMaterial(category,sourceId);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
        

        [HttpGet]
        public async Task<IActionResult> GetSourceMaterialNames([FromHeader] string categoryName)
        {
            var response = new Response();
            if (categoryName == null)
            {
                response.StatusCode = 500;
                response.StatusMessages.Add("provide a Category name");
                return BadRequest(response);
            }
            var result = await _sourceMaterialService.GetAllSourceMaterials(categoryName);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestionsByName([FromForm] ICollection<MaterialDTO.QuestionModel> questions, [FromHeader] string sourceMaterialName, [FromHeader] string category)
        {
            var result = await _sourceMaterialService.AddQuestion(questions, sourceMaterialName, category);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestionsByGuid([FromForm] ICollection<MaterialDTO.QuestionModel> questions, [FromHeader] Guid sourceId)
        {
            if (questions == null) { return BadRequest("Payload not received"); }
            var result = await _sourceMaterialService.AddQuestion(questions, sourceId);
            return Ok(result);
        }
    }
}

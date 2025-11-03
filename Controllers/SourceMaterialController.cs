using Boompa.DTO;
using Boompa.Entities;
using Boompa.Implementations.Services;
using Boompa.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Boompa.Controllers
{
    //[Authorize(Roles = "Admin")]
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
        
        public async Task<IActionResult> AddSourceMaterial([FromForm] MaterialDTO.ArticleModel sourceMaterial)
        {
            try
            {
                if(sourceMaterial== null ) { return BadRequest("Material not received by server"); }
                var result = await _sourceMaterialService.AddSourceMaterial(sourceMaterial);

                return Ok(result);

                
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
        public async Task<IActionResult> AddQuestion([FromForm]MaterialDTO.QuestionModel model, [FromHeader] int sourceMaterialId)
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
        public async Task<IActionResult> GetSourceMaterial([FromHeader]string sourceMaterialName, [FromHeader]string category)
        {
            if (sourceMaterialName == null) { return BadRequest("SourceMaterialName not provided"); }
            try
            {
                var result  = await _sourceMaterialService.GetSourceMaterial(sourceMaterialName, category);
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
    }
}

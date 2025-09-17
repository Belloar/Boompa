using Boompa.DTO;
using Boompa.Entities;
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
            _sourceMaterialService = sourceMaterialService;             /****************************/
        }
        //[Authorize(Roles ="creator")]
        //[Authorize(Roles = "sub-creator")]
        [HttpPost]
        
        public async Task<IActionResult> AddSourceMaterial([FromForm] MaterialDTO sourceMaterial)//,ICollection<MaterialDTO.QuestionModel> queModel || queModel == null ,queModel
        {
            var articleModel = new MaterialDTO.ArticleModel
            {
                Category = sourceMaterial.Category,
                SourceMaterialName = sourceMaterial.SourceMaterialName,
                Text = sourceMaterial.Text,
                Creator = sourceMaterial.Creator,
                CreatedOn = sourceMaterial.CreatedOn,
                RawFiles = sourceMaterial.RawFiles,
            };
            

            try
            {
                if(sourceMaterial== null ) { return BadRequest("Material not received by server"); }
                var result = await _sourceMaterialService.AddSourceMaterial(articleModel);

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
        public async Task<IActionResult> AddQuestion([FromForm] ICollection<MaterialDTO.QuestionModel> model, [FromHeader] int sourceMaterialId)
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
    }
}

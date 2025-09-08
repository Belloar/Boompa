using Boompa.DTO;
using Boompa.Entities;
using Boompa.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        //[Authorize(Roles ="creator")]
        //[Authorize(Roles = "sub-creator")]
        [HttpPost]
        
        public async Task<IActionResult> AddSourceMaterial([FromForm] MaterialDTO.ArticleModel sourceMaterial)
        {
            try
            {
                if(sourceMaterial== null) { return BadRequest("Material not received by server"); }
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
    }
}

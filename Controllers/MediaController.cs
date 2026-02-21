using Boompa.DTO;
using Boompa.Interfaces.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boompa.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MediaController(ICloudService cloudService) : ControllerBase
    {
        private readonly ICloudService _cloudService = cloudService;

        [HttpPost]
        public async Task<IActionResult> UploadMedia([FromForm] MaterialDTO.TinyMedia file)
        {
            var result = await _cloudService.UploadFileAsync(file.file);
            return Ok(new { location = result});
        }
    }
}

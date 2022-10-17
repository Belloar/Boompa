using Boompa.DTO;
using Boompa.Entities;
using Boompa.Exceptions;
using Boompa.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boompa.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;
        public AdminController(IAdminService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmin([FromForm] AdminDTO.CreateModel model)
        {
            if(model == null)return BadRequest("please fill in credentials");
            var cancellationToken = new CancellationToken();
            try
            {
                var result = await _service.CreateAdminAsync(model, cancellationToken);
                if(result == 0)return StatusCode(500,"something don sup sha");
                return Ok("Admin created");
            }
            catch(IdentityException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(ServiceException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

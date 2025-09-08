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
        //[Authorize(Roles = "Admin")]
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
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAdmin([FromQuery] int UserId)
        {
            if (UserId == 0) return BadRequest("No id found");
            try
            {
               var admin = await _service.GetAdminAsync(UserId);
                return Ok(admin);
                
            }
            catch(IdentityException  ex)
            {
                return NotFound(ex.Message);
            }
            catch(ServiceException ex)
            {
                return StatusCode(500,ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAdmin([FromForm] AdminDTO.UpdateModel model)
        {
            if (model == null) return BadRequest("No data received");
            try
            {
                var cancellationToken = new CancellationToken();
                var result = await _service.UpdateAdminAsync(model, cancellationToken);
                return Ok(result);
            }
            catch(ServiceException ex)
            {
                return StatusCode(500,ex.Message);
            }
            catch(IdentityException ex )
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

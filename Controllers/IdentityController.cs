using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Boompa.Interfaces;
using Boompa.Auth;
using Boompa.Exceptions;
using Boompa.DTO;

namespace Boompa.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        
        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
            
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> UserLogin([FromHeader]string username,[FromHeader]string password, [FromHeader] string page)
        {
            var response = await _identityService.UserLogin(username, password,page);
            return Ok(response);
        }

        [HttpGet("{pageNumber}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers([FromRoute] int pageNumber)
        {
            var result = await _identityService.GetUsersAsync(pageNumber);
            return Ok(result);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> SendVerificationCode(string email)
        {
            var response = new Response();
            response.StatusCode = 200;

            await _identityService.SendEmail(email);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmail([FromBody] IdentityDTO.EmailModel model)
        {
            var response = await _identityService.VerifyEmail(model.Email, model.UserInput);
            return Ok(response);
        }





    }
}

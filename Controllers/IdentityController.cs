using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Boompa.Interfaces;
using Boompa.Auth;

namespace Boompa.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        //private readonly IHttpContextAccessor _context;
        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
            //_context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> UserLogin([FromQuery] IdentityDTO.UserLoginModel model)
        {
            if(model == null)
            {
                return BadRequest("the input fields are required");
            }
            var user = _identityService.AuthenticateUser(model.CheckString, model.Password);
            if(user == null)
            {
                return NotFound($"A user with the username {model.CheckString} does not exist");
            }
            
            return Ok("it be like say i dey work");
        }
    }
}

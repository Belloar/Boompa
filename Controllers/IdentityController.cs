using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Boompa.Interfaces;
using Boompa.Auth;
using Boompa.Exceptions;

namespace Boompa.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
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
            if(model == null) return BadRequest("No data received");
           
            var validUser = await _identityService.AuthenticateUser(model.SearchString, model.Password);
            if(validUser == null)
            {
                return NotFound($"A user with the username or email does not exist");
            }
            
            var token = await _identityService.GenerateToken(validUser);
            return Ok(token);
        }



        
    }
}

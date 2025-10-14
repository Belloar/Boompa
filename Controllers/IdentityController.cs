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
    [Authorize]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        //private readonly IHttpContextAccessor _Httpcontext;
        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
            //_Httpcontext = httpContext;
        }

        [AllowAnonymous]
        [HttpGet("{username}/{password}")]
        public async Task<IActionResult> UserLogin(string username,string password)
        {
            if (username == null && password == null) { return BadRequest("No data received"); }
            try
            {
                var response = new Response();

                var validUser = await _identityService.AuthenticateUser(username, password);
                if (validUser == null)
                {
                    return NotFound($"A user with the username or email does not exist");
                }
                response.StatusCode = 200;
                response.Data=validUser;
                response.StatusMessages.Add($"Welcome {validUser.UserName}");
                return Ok(response);
            }
            catch(IdentityException ex)
            {
                return StatusCode(500,ex.Message);
            }
        }



        
    }
}

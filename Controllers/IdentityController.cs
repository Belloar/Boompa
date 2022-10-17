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
            if(model == null) return BadRequest("the input fields are required");
           
            var user = await _identityService.AuthenticateUser(model.CheckString, model.Password);
            if(user == null)
            {
                return NotFound($"A user with the username or email does not exist");
            }
            var validUser = new IdentityDTO.ValidUserModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                IsAuthenticated = true,
                Token = await _identityService.GenerateToken(user),
                Email = user.Email,
            };
            foreach (var role in user.Roles)
            {
                validUser.Roles.Add(role);
            }
            return Ok(validUser);
        }


        
    }
}

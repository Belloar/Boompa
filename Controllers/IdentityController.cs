using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Boompa.Interfaces;
using Boompa.Auth;


namespace Boompa.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize("User")]
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
            foreach (var role in user.UserRoles)
            {
                validUser.Roles.Add(role);
            }
            return Ok(validUser);
        }
        [HttpPost]
        public async Task<IActionResult> Createuser([FromBody] IdentityDTO.CreateRequestModel model)
        {
            if(model == null) return BadRequest("input credentials please");
            var cancellationToken = new CancellationToken();
            var result =  await _identityService.CreateAsync(model, cancellationToken);
            if(result == 0) return BadRequest("I no sabi wetin sup comrade");//when i know what errors might occur rewrite this place
            return Ok("account successfully created");
        }
    }
}

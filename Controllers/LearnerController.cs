using Boompa.DTO;
using Boompa.Exceptions;
using Boompa.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Boompa.DTO;

namespace Boompa.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LearnerController : ControllerBase
    {
        private readonly ILearnerService _service;
        public LearnerController(ILearnerService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize("Learner")]
        public async Task<IActionResult> CreateLearner([FromBody] LearnerDTO.CreateRequestModel model)
        {
            var cancellationToken = new CancellationToken();
            if (model == null) return BadRequest("all fields must be filled");
            try
            {
                var result = await _service.CreateLearner(model, cancellationToken);
                return Ok("You have completed your profile setup");
                
            }
            catch(ServiceException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        [Authorize("Learner")]
        [Authorize("Administrator")]
        public async Task<IActionResult> DeleteLearner([FromQuery] int id)
        {
            var cancellationToken = new CancellationToken();
            var result = await _service.DeleteLearner(id, cancellationToken);
            if (result != 0) return Ok("profile deleted");
            throw new ServiceException();
        }


        [HttpGet("{id}")]
        [Authorize("Administrator")]
        public async Task<IActionResult> GetLearner([FromQuery]int id)
        {
            var learner = await _service.GetLearner(id);
            if (learner  == null) return NotFound("a user with this username or password does not exist");
            return Ok(learner);
        }


        [HttpGet("{alpha:checkString}")]//revisit data types of controller variables
        [Authorize("Learner")]
        [Authorize("Administrator")]
        public async Task<IActionResult> GetLearner([FromQuery] string checkString)
        {
            try
            {
                var learner = await _service.GetLearner(checkString);
                return Ok(learner);
            }
            catch(ServiceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(IdentityException ex)
            {
                return NotFound(ex.Message);
            }
        }
       
    }
}

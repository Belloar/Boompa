using Boompa.Auth;
using Boompa.DTO;
using Boompa.Exceptions;
using Boompa.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


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
        public async Task<IActionResult> CreateLearner([FromForm] LearnerDTO.CreateRequest model)
        {
            var cancellationToken = new CancellationToken();
            if (model == null) return BadRequest("all fields must be filled");
            try
            {
                var result = await _service.CreateLearner(model, cancellationToken);
                return Ok("You have completed your profile setup");
            }
            catch (ServiceException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        //[Authorize(Roles = "Learner")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteLearner([FromQuery] int id)
        {
            var cancellationToken = new CancellationToken();
            var result = await _service.DeleteLearner(id, cancellationToken);
            if (result != 0) return Ok("profile deleted");
            throw new ServiceException();
        }


        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetLearnerById([FromQuery] int id)
        {
            try
            {
                var learner = await _service.GetLearner(id);
                return Ok(learner);
            }
            catch (ServiceException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        //[Authorize(Roles = "Administrator")]
        public IActionResult TestMethod()
        {
            return Ok("warri no dey carry last");

        }


        [HttpGet]//revisit data types of controller variables
        //[Authorize(Roles ="Learner")]
        
        public async Task<IActionResult> GetLearner([FromQuery] string searchString)
        {
            try
            {
                var learner = await _service.GetLearner(searchString);
                return Ok(learner);
            }
            catch (ServiceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (IdentityException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet]
        //[Authorize(Roles = "Adminsitrator")]
        public async Task<IActionResult> GetLearners()
        {
            var learners = await _service.GetLearners();
            return Ok(learners);
        }

        [HttpGet]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetLearnersInfo()
        {
            try
            {
                var learnersInfo = await _service.GetLearnersInfo();
                return Ok(learnersInfo);
            }
            catch (ServiceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        //[Authorize(Roles = "learner")]
        public async Task<IActionResult> UpdateLearner([FromBody] LearnerDTO.UpdateInfo updateInfo)
        {
            try
            {
                var cancellationToken = new CancellationToken();
                var result = await _service.UpdateLearner(updateInfo, cancellationToken);
                if (result == 1) return Ok("update successful");
                return BadRequest(result);
            }
            catch (ServiceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLearnerStats([FromBody] LearnerDTO.UpdateStats model)
        {
            if (model == null) return BadRequest("No info found");

            try
            {
                var cancellationToken = new CancellationToken();
                var result = await _service.UpdateLearner(model, cancellationToken);
                if (result == 1) return NoContent();
                return BadRequest("An error occured during the process");
            }
            catch(ServiceException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

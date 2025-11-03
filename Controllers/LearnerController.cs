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
        private readonly ILearnerService _learnerService;
        public LearnerController(ILearnerService service)
        {
            _learnerService = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLearner([FromBody] LearnerDTO.CreateRequest model)
        {
            
            if (model == null) return BadRequest("all fields must be filled");
            try
            {
                var result = await _learnerService.CreateLearner(model);
                return Ok("You have completed your profile setup");
            }
            catch (ServiceException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Learner,admin")]
        
        public async Task<IActionResult> DeleteLearner(int id)
        {
            
            var result = await _learnerService.DeleteLearner(id);
            if (result != 0) return Ok("profile deleted");
            throw new ServiceException();
        }

        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetLearnerById([FromBody] int id)
        {
            try
            {
                var learner = await _learnerService.GetLearner(id);
                return Ok(learner);
            }
            catch (ServiceException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetLearner([FromHeader] string searchString)
        {
            try
            {
                var learner = await _learnerService.GetLearner(searchString);
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
        public async Task<IActionResult> GetLearners()
        {
            var learners = await _learnerService.GetLearners();
            return Ok(learners);
        }

        [HttpGet]
        
        public async Task<IActionResult> GetLearnersInfo()
        {
            try
            {
                var learnersInfo = await _learnerService.GetLearnersInfo();
                return Ok(learnersInfo);
            }
            catch (ServiceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "learner")]
        public async Task<IActionResult> UpdateLearner([FromBody] LearnerDTO.UpdateInfo updateInfo)
        {
            try
            {
                
                var result = await _learnerService.UpdateLearner(updateInfo);
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
                
                var result = await _learnerService.UpdateLearner(model);
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

using Boompa.DTO;
using Boompa.Entities;
using Boompa.Exceptions;
using Boompa.Interfaces.IService;
using Boompa.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boompa.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ILearnerService _learnerService;
        private readonly ISourceMaterialService _sourceMaterialService;
        public AdminController(IAdminService adminService, ISourceMaterialService sourceMaterialService, ILearnerService learnerService)
        {
            _adminService = adminService;
            _sourceMaterialService = sourceMaterialService;
            _learnerService = learnerService;
        }

        [HttpPost]
        
        public async Task<IActionResult> CreateAdmin([FromForm] AdminDTO.CreateModel model)
        {
            if(model == null)return BadRequest("please fill in credentials");
            
            try
            {
                var result = await _adminService.CreateAdminAsync(model);
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
        
        public async Task<IActionResult> GetAdmin([FromQuery] int UserId)
        {
            if (UserId == 0) return BadRequest("No id found");
            try
            {
               var admin = await _adminService.GetAdminAsync(UserId);
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
        
        public async Task<IActionResult> UpdateAdmin([FromForm] AdminDTO.UpdateModel model)
        {
            if (model == null) return BadRequest("No data received");
            try
            {
                
                var result = await _adminService.UpdateAdminAsync(model);
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

        [HttpPost]
        public async Task<IActionResult> AddSourceMaterial([FromForm] MaterialDTO.ArticleModel material)
        {
            try
            {
                var response = await _sourceMaterialService.AddSourceMaterial(material);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetLearnerById([FromHeader] int id)
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

        [HttpGet]
        public async Task<IActionResult> GetLearners()
        {
            var learners = await _learnerService.GetLearners();
            return Ok(learners);
        }

        

    }
}

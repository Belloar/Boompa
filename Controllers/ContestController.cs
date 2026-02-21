using Boompa.DTO;
using Boompa.Interfaces.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Boompa.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContestController : ControllerBase
    {
        private readonly IContestRecordService _contestService;

        public ContestController(IContestRecordService contestService)
        {
            _contestService = contestService;
        }

        [HttpPost]
        public async Task<IActionResult> AddRecord([FromBody] ContestRecordDTO.CreateRecord record)
        {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email).ToString();
            if (email != null)
            {
                await _contestService.AddRecord(record, email);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public IActionResult UpdateRecord([FromBody] ContestRecordDTO.UpdateRecord model)
        {
            var value = HttpContext.User.FindFirstValue(ClaimTypes.Email).ToString();
            if (value != null)
            {
                var result = _contestService.UpdateRecord(model, value);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecords()
        {
            var records = await _contestService.GetAllRecords();
            return Ok(records);

        }




    }
}

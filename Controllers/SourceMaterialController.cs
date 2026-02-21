using Boompa.DTO;
using Boompa.Entities;
using Boompa.Implementations.Services;
using Boompa.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace Boompa.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SourceMaterialController(ISourceMaterialService sourceMaterialService, IAIQuestionGenerator questionGenerator) : ControllerBase
    {
        private readonly ISourceMaterialService _sourceMaterialService = sourceMaterialService;

        private readonly IAIQuestionGenerator _questionGenerator = questionGenerator;

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSourceMaterial([FromForm] MaterialDTO.ArticleModel sourceMaterial)
        {
            try
            {
                var creator = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (sourceMaterial == null) { return BadRequest("Material not received by server"); }

                if (creator != null)
                {
                    var result = await _sourceMaterialService.AddSourceMaterial(sourceMaterial, creator);
                    return Ok(result);
                }
                else
                {
                    var result = await _sourceMaterialService.AddSourceMaterial(sourceMaterial);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNewSourceMaterial([FromForm] MaterialDTO.TinyModel model)
        {
            var result = await _sourceMaterialService.AddSourceMaterial(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] string category)
        {
            if (category == null) { return BadRequest("Material not received"); }
            try
            {
                var result = await _sourceMaterialService.AddCategory(category);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GenerateQuestion([FromBody] MaterialDTO.AIQueGenDTO model)
        {
            var result = _questionGenerator.GenerateQuestions(model.Material, model.Prompt);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion([FromForm] MaterialDTO.QuestionModel model, [FromHeader] Guid sourceMaterialId)
        {
            if (model == null) { return BadRequest("Material not received"); }
            try
            {
                var result = await _sourceMaterialService.AddQuestion(model, sourceMaterialId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSourceMaterial([FromHeader] Guid sourceId, [FromHeader] string category)
        {
            if (sourceId == null) { return BadRequest("SourceMaterialName not provided"); }
            try
            {
                var result = await _sourceMaterialService.GetSourceMaterial(category, sourceId);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetSourceMaterialNames([FromHeader] string categoryName)
        {
            var response = new Response();
            if (categoryName == null)
            {
                response.StatusCode = 500;
                response.StatusMessages.Add("provide a Category name");
                return BadRequest(response);
            }
            var result = await _sourceMaterialService.GetAllSourceMaterials(categoryName);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestionsByName([FromForm] ICollection<MaterialDTO.QuestionModel> questions, [FromHeader] string sourceMaterialName, [FromHeader] string category)
        {
            var result = await _sourceMaterialService.AddQuestion(questions, sourceMaterialName, category);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestionsByGuid([FromForm] ICollection<MaterialDTO.QuestionModel> questions, [FromHeader] Guid sourceId)
        {
            if (questions == null) { return BadRequest("Payload not received"); }
            var result = await _sourceMaterialService.AddQuestion(questions, sourceId);
            return Ok(result);
        }


        //this is an ai generated webhook endpoint. i'm leaving this method as is for now just for testing i'll learn how it works properly
        //[Route("api/typeform")]
        //[HttpPost("webhook")]
        //public async Task<IActionResult> ReceiveWebhook([FromBody] JsonElement payload)
        //{
        //    try
        //    {
        //        using var reader = new StreamReader(Request.Body);
        //        var body = await reader.ReadToEndAsync();

        //        Console.WriteLine("Received from Typeform:");
        //        Console.WriteLine(body);

        //        // Deserialize if needed
        //        var json = JsonDocument.Parse(body);

        //        // TODO: Save to database here


        //        return Ok(new { message = "Webhook received successfully" });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions that occur during processing
        //        return StatusCode(500, new { message = "An error occurred while processing the webhook", error = ex.Message });
        //    }

        //}
    }
}

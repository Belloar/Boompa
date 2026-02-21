using Boompa.DTO;
using Boompa.Interfaces.IService;
using System.Net.Http.Headers;

namespace Boompa.Implementations.Services
{
    public class OpenAIService(HttpClient httpClient, IConfiguration config) : IAIQuestionGenerator
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IConfiguration _config = config;
        public async Task<Response> GenerateQuestions(string material, string prompt)
        {
            var section = _config.GetSection("OpenAI");
            var apiKey = _config["openAI:apiKey"];
            var url = section["URL"];
            var model = section["Model"];

            var response = new Response();
            try
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                var requestBody = new
                {
                    Model = model,
                    input = $"{prompt}\n\n{material}",
                };
                //the size of the material parameter should be limited to reduce the cost of the API call,
                var request = await _httpClient.PostAsJsonAsync(url, requestBody);

                var result = await request.Content.ReadAsStringAsync();

                response.StatusCode = 200;
                response.StatusMessages.Add("Questions generated successfully");
                response.Data = result;

                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 200;
                response.StatusMessages.Add($"Error generating questions: {ex.Message}");
                return response;
            }
        }
    }
}

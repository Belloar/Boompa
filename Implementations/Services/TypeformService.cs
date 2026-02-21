using Boompa.DTO;
using Boompa.Interfaces.IService;
using System.Net.Http.Headers;

namespace Boompa.Implementations.Services
{
    public class TypeformService(HttpClient httpClient,IConfiguration configuration) : IAIQuestionGenerator
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IConfiguration _config = configuration;
        public async Task<Response> GenerateQuestions(string material, string prompt)
        {
            throw new NotImplementedException();
            var section = _config.GetSection("Typeform");
            var apiKey = _config["openAI:ApiKey"];
            var url = section["URL"];

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",apiKey);

        }
    }
}

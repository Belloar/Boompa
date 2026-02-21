using Boompa.DTO;

namespace Boompa.Interfaces.IService
{
    public interface IAIQuestionGenerator
    {
        Task<Response> GenerateQuestions(string material,string prompt);
    }
}

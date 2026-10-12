using Boompa.DTO;
using Boompa.Entities.Question;
using Boompa.Entities.Question.Question;
using Boompa.Interfaces.Abstractions;

namespace Boompa.Implementations.Services
{
    public class MCQuestion : IQuestion
    {
        public BaseQuestion Create(MaterialDTO.QuestionDTO model)
        {
            var question = new McqQuestion()
            {
                Prompt = model.TextQuestion,
                SourceMaterialId = Guid.NewGuid()
                

            };

            return question;
        }
    }
}

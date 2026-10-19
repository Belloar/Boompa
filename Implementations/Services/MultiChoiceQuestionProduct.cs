using Boompa.DTO;
using Boompa.Entities.Question;
using Boompa.Entities.Question.Question;
using Boompa.Interfaces.Abstractions;

namespace Boompa.Implementations.Services
{
    public class MultiChoiceQuestionProduct : IQuestion
    {
        public BaseQuestion Create(MaterialDTO.QuestionDTO model)
        {
            var question = new MultiChoiceQuestion()
            {
                Prompt = model.TextQuestion,
                SourceMaterialId = Guid.NewGuid()
                

            };

            return question;
        }
    }
}

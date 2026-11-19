using Boompa.DTO;
using Boompa.Entities.Question;
using Boompa.Entities.Question.Question;
using Boompa.Interfaces.Abstractions;

namespace Boompa.Implementations.Services.AppServices
{
    public class MultiChoiceQuestionProduct : IQuestion
    {
        public BaseQuestion Create(MaterialDTO.QuestionModel model)
        {
            var question = new MultiChoiceQuestion()
            {
                Prompt = model.TextDescription,
                

            };

            return question;
        }
    }
}

using Boompa.DTO;
using Boompa.Entities.Question;
using Boompa.Interfaces.Abstractions;

namespace Boompa.Implementations.Services
{
    public class MultiResponseQuestionProduct : IQuestion
    {
        public BaseQuestion Create(MaterialDTO.QuestionDTO model)
        {
            var question = new MultipleResponseQuestion
            {
                Prompt = model.TextQuestion,
                SourceMaterialId = Guid.NewGuid()
            };

            return question;
        }
    }
}

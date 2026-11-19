using Boompa.DTO;
using Boompa.Entities.Question;
using Boompa.Interfaces.Abstractions;

namespace Boompa.Implementations.Services.AppServices
{
    public class MultiResponseQuestionProduct : IQuestion
    {
        public BaseQuestion Create(MaterialDTO.QuestionModel model)
        {
            var question = new MultipleResponseQuestion
            {
                Prompt = model.TextDescription,
                SourceMaterialId = model.SourceMaterialId 
            };

            return question;
        }
    }
}

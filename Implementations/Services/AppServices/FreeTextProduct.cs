using Boompa.DTO;
using Boompa.Entities.Question;
using Boompa.Interfaces.Abstractions;

namespace Boompa.Implementations.Services.AppServices
{
    public class FreeTextProduct : IQuestion
    {
        public BaseQuestion Create(MaterialDTO.QuestionModel model)
        {
            var question = new FreeTextQuestion
            {
                Prompt = model.TextDescription,
            };
            return question;
        }
    }
}

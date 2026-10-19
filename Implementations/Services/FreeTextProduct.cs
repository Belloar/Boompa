using Boompa.DTO;
using Boompa.Entities.Question;
using Boompa.Interfaces.Abstractions;

namespace Boompa.Implementations.Services
{
    public class FreeTextProduct : IQuestion
    {
        public BaseQuestion Create(MaterialDTO.QuestionDTO model)
        {
            var question = new FreeTextQuestion
            {
                Prompt = model.TextQuestion
            };
            return question;
        }
    }
}

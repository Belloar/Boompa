using Boompa.DTO;
using Boompa.Entities.Question;
using Boompa.Interfaces.Abstractions;

namespace Boompa.Implementations.Services
{
    public abstract class QuestionBuilder
    {
        protected abstract IQuestion Build();

        public BaseQuestion BuildQuestion(MaterialDTO.QuestionModel model)
        {
            var builder = Build();
            var question = builder.Create(model);
            return question;
        }
    }
}

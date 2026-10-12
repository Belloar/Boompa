using Boompa.DTO;
using Boompa.Interfaces.Abstractions;

namespace Boompa.Implementations.Services
{
    public abstract class QuestionBuilder
    {
        protected abstract IQuestion Build();

        public void BuildQuestion(MaterialDTO.QuestionDTO model)
        {
            var builder = Build();
            var question = builder.Create(model);
        }
    }
}

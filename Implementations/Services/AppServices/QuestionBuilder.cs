using Boompa.DTO;
using Boompa.Entities.Question;
using Boompa.Interfaces.Abstractions;

namespace Boompa.Implementations.Services.AppServices
{
    public enum QuestionType
    {
        FreeText,
        MultipleChoice,
        MultipleResponse
    }

    public abstract class QuestionBuilder
    {
        //Concrete implementations of this class
        //will provide the specific IQuestion implementation to use for building the question.
        protected abstract IQuestion Build();
        public QuestionType Type { get; set; }

        public BaseQuestion BuildQuestion(MaterialDTO.QuestionModel model)
        {
            var builder = Build();
            var question = builder.Create(model);
            return question;
        }
    }
}

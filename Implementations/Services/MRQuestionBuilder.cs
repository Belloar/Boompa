using Boompa.Interfaces.Abstractions;

namespace Boompa.Implementations.Services
{
    public class MRQuestionBuilder : QuestionBuilder
    {
        protected override IQuestion Build()
        {
            return new MultiResponseQuestionProduct();
        }
    }
}

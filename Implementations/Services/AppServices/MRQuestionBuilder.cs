using Boompa.Interfaces.Abstractions;

namespace Boompa.Implementations.Services.AppServices
{
    public class MRQuestionBuilder : QuestionBuilder
    {
        protected override IQuestion Build()
        {
            return new MultiResponseQuestionProduct();
        }
    }
}

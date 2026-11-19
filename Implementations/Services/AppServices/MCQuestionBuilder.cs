using Boompa.Interfaces.Abstractions;

namespace Boompa.Implementations.Services.AppServices
{
    public class MCQuestionBuilder : QuestionBuilder
    {
        protected override IQuestion Build() => new MultiChoiceQuestionProduct();

    }
}

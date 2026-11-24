using Boompa.Interfaces.Abstractions;

namespace Boompa.Implementations.Services.AppServices
{
    public class MCQuestionBuilder : QuestionBuilder
    {
        public override QuestionType Type => QuestionType.MultipleChoice;

        protected override IQuestion Build(QuestionType type) => new MultiChoiceQuestionProduct();

    }
}

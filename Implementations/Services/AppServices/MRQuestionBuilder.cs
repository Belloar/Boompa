using Boompa.Interfaces.Abstractions;

namespace Boompa.Implementations.Services.AppServices
{
    public class MRQuestionBuilder : QuestionBuilder
    {
        public override QuestionType Type => QuestionType.MultipleResponse;
        protected override IQuestion Build(QuestionType type)
        {
            return new MultiResponseQuestionProduct();
        }
    }
}

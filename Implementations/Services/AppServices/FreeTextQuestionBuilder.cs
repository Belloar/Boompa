using Boompa.Interfaces.Abstractions;

namespace Boompa.Implementations.Services.AppServices
{
    public class FreeTextQuestionBuilder : QuestionBuilder
    {
        public override QuestionType Type => QuestionType.FreeText;
        protected override IQuestion Build(QuestionType type) => new FreeTextProduct();
        
    }
}

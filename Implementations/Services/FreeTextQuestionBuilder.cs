using Boompa.Interfaces.Abstractions;

namespace Boompa.Implementations.Services
{
    public class FreeTextQuestionBuilder : QuestionBuilder
    {
        protected override IQuestion Build() => new FreeTextProduct();
        
    }
}

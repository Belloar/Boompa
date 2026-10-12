using Boompa.Interfaces.Abstractions;

namespace Boompa.Implementations.Services
{
    public class MCQBuilder : QuestionBuilder
    {
        protected override IQuestion Build() => new MCQuestion();

    }
}

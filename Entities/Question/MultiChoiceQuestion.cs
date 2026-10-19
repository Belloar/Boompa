using Boompa.Entities.Option;

namespace Boompa.Entities.Question.Question
{
    public class MultiChoiceQuestion : BaseQuestion 
    {
        public string Prompt { get; set; } = default!;
        public ICollection<BaseOption> Option { get; set; } = default!;

    }
}


namespace Boompa.Entities.Question.Question
{
    public class MultiChoiceQuestion : BaseQuestion 
    {
        public string Prompt { get; set; } = default!;
        public ICollection<Option> Options { get; set; } = default!;

    }
}

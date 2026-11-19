
namespace Boompa.Entities.Question
{
    public class MultipleResponseQuestion : BaseQuestion
    {
        public string Prompt { get; set; } = default!;
        public ICollection<Option> Options = [];
    }
}

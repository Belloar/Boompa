using Boompa.Entities.Option;

namespace Boompa.Entities.Question
{
    public class MultipleResponseQuestion : BaseQuestion
    {
        public string Prompt { get; set; } = default!;
        public ICollection<BaseOption> Options = [];
    }
}

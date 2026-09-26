using Boompa.Entities.Option;

namespace Boompa.Entities.Question
{
    public class FreeTextQuestion : BaseQuestion
    {
        public string Prompt { get; set; } = default!;
        public BaseOption Answer { get; set; } = default!;
    }
}

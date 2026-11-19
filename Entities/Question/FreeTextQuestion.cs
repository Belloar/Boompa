
namespace Boompa.Entities.Question
{
    public class FreeTextQuestion : BaseQuestion
    {
        public string Prompt { get; set; } = default!;
        public Option Answer { get; set; } = default!;
    }
}

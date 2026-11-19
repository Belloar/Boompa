using Boompa.Entities.Base;
using Boompa.Entities.Question;

namespace Boompa.Entities
{
    public enum OptionType
    {
        TextOption = 1,
        MediaOption
    }

    public enum MediaType
    {
        Image = 1,
        Video,
        Audio
    }
    public class Option : AuditableEntity
    {
        public Guid QuestionId { get; set; }
        public BaseQuestion Question { get; set; } = default!;
        public OptionType OptionType { get; set; } = default!;
        public string Value { get; set; } = default!;
        public MediaType? MediaType { get; set; } = null;
        public bool IsAnswer { get; set; } = false;
    }
}

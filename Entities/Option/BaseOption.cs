using Boompa.Entities.Base;
using Boompa.Entities.Question;

namespace Boompa.Entities.Option
{
    public abstract class BaseOption : AuditableEntity
    {
        public Guid QuestionId { get; set; }
        public BaseQuestion Question { get; set; } = default!;
        public bool IsAnswer { get; set; } = default!;
    }
}

using Boompa.Entities.Base;

namespace Boompa.Entities
{
    public class QuestionPhoto : AuditableEntity
    {
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int PhotoId { get; set; }
        public Photo Photo { get; set; }
    }
}

using Boompa.Entities.Base;

namespace Boompa.Entities
{
    public class Option:AuditableEntity
    {
        public int QuestionId { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public bool IsAnswer { get; set; }
    }
}

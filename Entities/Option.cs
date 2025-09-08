using Boompa.Entities.Base;

namespace Boompa.Entities
{
    public class Option:AuditableEntity
    {
        /*
         i made the "value" property of type object because the nature of the answer might not be only string it could be an image or something else sha
         */
        public int QuestionId { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public bool IsAnswer { get; set; }
    }
}

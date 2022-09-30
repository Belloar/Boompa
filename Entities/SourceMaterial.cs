using Boompa.Entities.Base;

namespace Boompa.Entities
{
    public class SourceMaterial : AuditableEntity
    {
        public int CategoryId { get; set; }
        public int QuestionId { get; set; }
        public string MediaType { get; set; }
        public string Path { get; set; }
    }
}

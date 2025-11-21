using Boompa.Entities.Base;
using Boompa.Enums;

namespace Boompa.Entities
{
    public class SourceMaterial : AuditableEntity 
    {
        
        public string Name { get; set; }  
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public ICollection<Question>? Questions { get; set; }
        public ICollection<string> Files { get; set; } = [];
    }
}

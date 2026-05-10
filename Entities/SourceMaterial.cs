using Boompa.Entities.Base;
using Boompa.Enums;

namespace Boompa.Entities
{
    public class SourceMaterial : AuditableEntity 
    {
        
        public string Name { get; set; } 
        public string Content { get; set; }
        public string Description { get; set; }
        public ICollection<CategorySourceMaterial> Categories { get; set; } = [];
        public ICollection<LearnerSourceMaterial> BookMarkers { get; set; } = [];
        public ICollection<Question> Questions { get; set; } = new HashSet<Question>();
    }
}

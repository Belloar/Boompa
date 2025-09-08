using Boompa.Entities.Base;
using Boompa.Enums;

namespace Boompa.Entities
{
    public class SourceMaterial : AuditableEntity 
    {
        
        public string Name { get; set; }     
        public string Category { get; set; }
        public string Content { get; set; }
        public ICollection<Question>? Questions { get; set; }
        public ICollection<SourceFileDetail>?  Images { get; set; } // collection of the filepaths for the images in the material
    }
}

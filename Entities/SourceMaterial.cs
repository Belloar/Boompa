using Boompa.Entities.Base;
using Boompa.Enums;

namespace Boompa.Entities
{
    public class SourceMaterial : AuditableEntity 
    {
        
        public string Name { get; set; }     
        public string Category { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// A brief summary of what the source material is to teach or what it is about. this will be displayed along with its name when a learner wishes to know what to learn from
        /// </summary>
        public string Description { get; set; }
        public ICollection<Question>? Questions { get; set; }
        public ICollection<SourceFileDetail>?  Files { get; set; } // collection of the file paths for the images in the material
        public ICollection<CloudSourceFileDetails>? CloudSourceFileDetails { get; set; }
    }
}

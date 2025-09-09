using Boompa.Entities.Base;

namespace Boompa.Entities
{
    public class Question:AuditableEntity
    {
        public int SourceMaterialId { get; set; }
        public string Description { get; set; }//the question
        public string Answer {  get; set; }// Answer to the question
        public ICollection<SourceFileDetail>? Files { get; set; } // Part of the question if the question requires images. *can be null 

    }
}

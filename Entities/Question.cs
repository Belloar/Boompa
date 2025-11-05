using Boompa.Entities.Base;

namespace Boompa.Entities
{
    public class Question:AuditableEntity
    {
        public int SourceMaterialId { get; set; }
        public SourceMaterial SourceMaterial { get; set; }
        public string Description { get; set; }//the question
        public string Answer {  get; set; }// Answer to the question
        public string Options {  get; set; } // options to be displayed along with the answer to the user
        //public ICollection<QuestionFileDetail>? Files { get; set; } // Part of the question if the question requires images. *can be null 
        public ICollection<CloudEvalFileDetails>? CloudEvalFileDetails { get; set; }

    }
}

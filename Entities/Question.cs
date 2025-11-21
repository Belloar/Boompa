using Boompa.Entities.Base;
using System.Text.Json.Serialization;

namespace Boompa.Entities
{
    public class Question:AuditableEntity
    {
        public Guid SourceMaterialId { get; set; }
        //[JsonIgnore]
        public SourceMaterial SourceMaterial { get; set; }
        public string Description { get; set; }//the question
        public string Answer {  get; set; }// Answer to the question
        public string Options {  get; set; } // options to be displayed along with the answer to the user
        public ICollection<string>? Files { get; set; }

    }
}

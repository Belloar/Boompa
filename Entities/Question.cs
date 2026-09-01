using Boompa.Entities.Base;
using System.Text.Json.Serialization;

namespace Boompa.Entities
{
    public class Question:AuditableEntity
    {
        public Guid SourceMaterialId { get; set; }
        public SourceMaterial SourceMaterial { get; set; }
        public string Description { get; set; } = default!;//the question
        public string Answer {  get; set; } = default!;// Answer to the question
        public string Options {  get; set; } = default!; // options to be displayed along with the answer to the user
        public string QuestionType { get; set; } = default!;
        public string OptionType { get; set; } = default!;

    }
}

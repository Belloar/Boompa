using Boompa.Entities.Base;
using System.Text.Json.Serialization;

namespace Boompa.Entities.Question
{
    public abstract class BaseQuestion:AuditableEntity
    {
        public Guid SourceMaterialId { get; set; }
        public SourceMaterial SourceMaterial { get; set; } = default!;

    }
}

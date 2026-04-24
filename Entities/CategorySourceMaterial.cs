using Boompa.Entities.Base;

namespace Boompa.Entities
{
    public class CategorySourceMaterial:BaseEntity
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid SourceMaterialId { get; set; }
        public SourceMaterial SourceMaterial { get; set; }
    }
}

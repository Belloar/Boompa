using Boompa.Entities.Base;

namespace Boompa.Entities
{
    public class Category:BaseEntity
    {
        public string Name {  get; set; }
        public ICollection<CategorySourceMaterial> SourceMaterials { get; set; } = [];
        public ICollection<CategoryLearner> CategoryLearners { get; set; } = [];
    }
}

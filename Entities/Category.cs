using Boompa.Entities.Base;

namespace Boompa.Entities
{
    public class Category:BaseEntity
    {
        public string Name {  get; set; }
        public ICollection<SourceMaterial> SourceMaterials { get; set; } = new HashSet<SourceMaterial>();
        public ICollection<CategoryLearner> CategoryLearners { get; set; } = [];
        public ICollection<CategoryChallenger> CategoryChallengers { get; set; } = [];
    }
}

using Boompa.Entities.Base;

namespace Boompa.Entities
{
    public class Question:AuditableEntity
    {
        public int CategoryId { get; set; }
        public string ArticleName { get; set; }
        public string Description { get; set; }
        public List<SourceMaterial> ImagesPath{ get; set; }
        public ICollection<Option> Options = new HashSet<Option>();

    }
}

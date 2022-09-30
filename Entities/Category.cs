using Boompa.Entities.Base;

namespace Boompa.Entities
{
    public class Category:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SourceMaterial> Materials { get; set; }
        public ICollection<Question> Questions { get; set; }
        
         
    }
}

using Boompa.Entities.Base;

namespace Boompa.Entities
{
    public class Category:AuditableEntity
    {
        public string Name { get; set; }
        public ICollection<Article> Materials { get; set; }
        public ICollection<Question> Questions { get; set; }
        
         
    }
}

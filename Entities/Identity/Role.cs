using Boompa.Entities.Base;

namespace Boompa.Entities.Identity
{
    public class Role:AuditableEntity
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
        public ICollection<UserRole> Users = new HashSet<UserRole>();

    }
    
}

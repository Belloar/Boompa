using Boompa.Entities.Base;

namespace Boompa.Entities.Identity
{
    public class UserRole:AuditableEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}

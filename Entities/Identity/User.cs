using Boompa.Entities.Base;

namespace Boompa.Entities.Identity
{
    public class User:AuditableEntity
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string HashSalt { get; set; } = default!;
        public bool IsEmailConfirmed { get; set; } = default!;
        
        public ICollection<UserRole> Roles { get; set; } = new HashSet<UserRole>();
    }
}

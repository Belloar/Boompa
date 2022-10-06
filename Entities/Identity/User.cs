using Boompa.Entities.Base;

namespace Boompa.Entities.Identity
{
    public class User:AuditableEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; } 
        public string PhoneNumber { get; set; }
        public string Hashsalt { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public Learner Learner { get; set; } 
        public Administrator Administrator { get; set; }
        public ICollection<UserRole> UserRoles = new HashSet<UserRole>();
    }
}

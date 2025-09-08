using Boompa.Entities.Base;
using Boompa.Entities.Identity;

namespace Boompa.Entities
{
    public class Admin:AuditableEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public byte[]? ProfilePicture { get; set; }
        
    }
}

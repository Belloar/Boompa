using Boompa.Entities.Base;
using Boompa.Entities.Identity;

namespace Boompa.Entities
{
    public class Admin:AuditableEntity
    {
        public string Email { get; set; } = default!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; } 
        public string? PhoneNumber { get; set; } 
        public string? ProfilePicture { get; set; }
        
    }
}

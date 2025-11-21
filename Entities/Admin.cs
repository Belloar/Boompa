using Boompa.Entities.Base;
using Boompa.Entities.Identity;

namespace Boompa.Entities
{
    public class Admin:AuditableEntity
    {
        public string Email { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string? ProfilePicture { get; set; }
        
    }
}

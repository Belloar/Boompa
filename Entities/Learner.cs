using Boompa.Entities.Base;
using Boompa.Entities.Identity;
using Boompa.Enums;

namespace Boompa.Entities
{
    public class Learner:AuditableEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public Rank Rank { get; set; }
        public string Status { get; set; }
    }
}

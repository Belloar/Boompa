using Boompa.Entities.Base;
using Boompa.Entities.Identity;
using Boompa.Enums;

namespace Boompa.Entities
{
    public class Learner:AuditableEntity
    {
        public string Email { get; set; } = default!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? School { get; set; }
        public string? ProfilePicture { get; set; }
        public string Rank { get; set; } = "Hatchling";
        public bool Status { get; set; }
        public string PhoneNumber { get; set; } = default!;
        public bool PaidInTickets { get; set; }
        public bool PaidInCoins { get; set; }
        public int TicketCount { get; set; }
        public int CoinCount { get; set; }
        public int ExpPoints { get; set; }
        public ICollection<LearnerCategory> LearnerCategories { get; set; }
        
        
       

    }
}

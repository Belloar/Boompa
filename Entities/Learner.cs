using Boompa.Entities.Base;
using Boompa.Entities.Identity;
using Boompa.Enums;

namespace Boompa.Entities
{
    public class Learner:AuditableEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? School { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public Rank Rank { get; set; }
        public int DiaryId { get; set; }
        public Diary? Diary { get; set; }
        public bool Status { get; set; }
        public bool PaidInTickets { get; set; }
        public bool PaidInCoins { get; set; }
        public int TicketCount { get; set; }
        public int CoinCount { get; set; }
        public int ExpPoints { get; set; }
        
        
       

    }
}

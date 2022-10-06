using Boompa.Entities.Base;
using Boompa.Entities.Identity;

namespace Boompa.Entities
{
    public class Diary:BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int TotalVists { get; set; }
        public string DisplayMessage { get; set; }
        public string CategoryVisited { get; set; }
        public int VisitId { get; set; }
        public List<Visit> Visit { get; set; }
        public int CoinsEarned { get; set; }
        public int TicketsEarned { get; set; }
        
        
    }
}

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
        public int Visits { get; set; }
       /* public Dictionary<string,int> CategoryVists { get; set; }
        public Dictionary<string, int> CategoriesToday { get; set; }*/
        public int CoinsEarned { get; set; }
        public int TicketsEarned { get; set; }
        public DateTime Duration { get; set; }
    }
}

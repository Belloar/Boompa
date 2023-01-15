using Boompa.Entities.Base;
using Boompa.Entities.Identity;

namespace Boompa.Entities
{
    public class Diary:BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int TotalVists { get; set; }
        public List<Visit>? Visit { get; set; }
        
        
        
    }
}



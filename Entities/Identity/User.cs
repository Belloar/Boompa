using Boompa.Entities.Base;

namespace Boompa.Entities.Identity
{
    public class User:BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public ICollection<Role> Roles = new HashSet<Role>();
    }
}

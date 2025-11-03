using Boompa.Entities.Base;

namespace Boompa.Entities
{
    public class Report : AuditableEntity
    {
        public string ReportedUser {  get; set; } 
        public string Description { get; set; }
    }
}

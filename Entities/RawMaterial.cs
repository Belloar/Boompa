using Boompa.Entities.Base;

namespace Boompa.Entities
{
    public class RawMaterial : AuditableEntity
    {
        public string Mediatype { get; set; }
        public string Path { get; set; }
    }
}

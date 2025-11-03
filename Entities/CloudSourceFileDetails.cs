using Boompa.Entities.Base;

namespace Boompa.Entities
{
    public class CloudSourceFileDetails : BaseEntity
    {
        public int SourceMaterialId { get; set; }
        public string Key { get; set; }
        public string FileType { get; set; }
    }
}

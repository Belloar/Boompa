using Boompa.Entities.Base;

namespace Boompa.Entities
{
    public class CloudEvalFileDetails : BaseEntity
    {
        public string Key { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public string FileType { get; set; }
    }
}

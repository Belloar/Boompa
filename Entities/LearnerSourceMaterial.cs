using Boompa.Entities.Base;

namespace Boompa.Entities
{
    public class LearnerSourceMaterial : BaseEntity
    {
        public Guid LearnerId { get; set; }
        public Learner Learner { get; set; }
        public Guid SourceMaterialId { get; set; }
        public SourceMaterial SourceMaterial { get; set; }
    }
}

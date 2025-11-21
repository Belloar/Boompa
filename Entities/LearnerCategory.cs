using Boompa.Entities.Base;

namespace Boompa.Entities
{
    public class LearnerCategory : BaseEntity
    {
        public Guid LearnerId  { get; set; }
        public Learner Learner { get; set; } = default!;

        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = default!;
    }
}

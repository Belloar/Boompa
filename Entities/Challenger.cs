using Boompa.Entities.Base;

namespace Boompa.Entities
{
    public class Challenger : BaseEntity
    {
        public Guid LearnerId { get; set; }
        public Learner Learner { get; set; }
        public ICollection<CategoryChallenger> CategoryChallengers { get; set; } = [];
    }
}

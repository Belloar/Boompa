using Boompa.Entities.Base;

namespace Boompa.Entities
{
    public class Visit : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public Guid LearnerId { get; set; }
        public Learner Learner { get; set; } = default!;
        public int CoinsEarned { get; set; }
        public int TicketsEarned { get; set; }
        public DateOnly Date { get; set; }
        public double Duration { get; set; }
    }
}

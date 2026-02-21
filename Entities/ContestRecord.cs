using Boompa.Entities.Base;
namespace Boompa.Entities
{
    public class ContestRecord : BaseEntity
    {
        public Guid LearnerId { get; set; }
        public Learner Learner { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public DateOnly Date {  get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string? SpeedAccuracyRatio { get; set; }
        public int ExpEarned { get; set; }
        public int NumberOfRounds { get; set; }
    }
}

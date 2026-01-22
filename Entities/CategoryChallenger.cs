namespace Boompa.Entities
{
    public class CategoryChallenger
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid ChallengerId { get; set; }
        public Challenger Challenger { get; set; }
        public string SpeedAccuracyRatio { get; set; }

    }
}

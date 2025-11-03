namespace Boompa.Entities
{
    public class Visit
    {
        public int Id { get; set; }
        public int DiaryId { get; set; }
        public Diary Diary { get; set; }
        public string CategoryVisited { get; set; }
        public int CoinsEarned { get; set; }
        public int TicketsEarned { get; set; }
        public DateOnly Date { get; set; }
        public DateTime Duration { get; set; }
    }
}

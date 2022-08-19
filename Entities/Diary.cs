namespace Boompa.Entities
{
    public class Diary
    {
        public int Id { get; set; }
        public int TotalVists { get; set; }
        public string DisplayMessage { get; set; }
        public Dictionary<Category, int> Vists { get; set; }
    }
}

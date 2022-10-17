namespace Boompa.Entities
{
    public class Article : RawMaterial 
    {
        public int CategoryId { get; set; }
        public ICollection<Question> Questions { get; set; }

    }
}

namespace Boompa.Entities
{
    public class Photo : RawMaterial 
    {
        public int QuestionId { get; set; }
        public ICollection<QuestionPhoto> Questions { get; set; }

    }
}

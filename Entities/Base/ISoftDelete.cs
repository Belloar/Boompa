namespace Boompa.Entities.Base
{
    public interface ISoftDelete
    {
       public string DeletedBy { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; }

    }
}

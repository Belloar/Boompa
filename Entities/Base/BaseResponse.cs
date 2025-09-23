namespace Boompa.Entities.Base
{
    public class BaseResponse
    {
        public int StatusCode { get; set; }
        public ICollection<string> StatusMessages { get; set; } = new HashSet<string>();
        public object? Data { get; set; }
    }
}

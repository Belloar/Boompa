namespace Boompa.Entities.Base
{
    public class BaseResponse
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public object Data { get; set; }
    }
}

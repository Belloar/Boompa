namespace Boompa.Entities.Option
{
    public class MediaOption : BaseOption
    {
        public string FileUrl { get; set; } = default!;
        public string ContentType { get; set; } = default!;
        public FileInfo File { get; set; } = default!;
    }
}

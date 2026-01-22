namespace Boompa.DTO
{
    public class FileDTO
    {
        public string Index { get; set; }
        public IFormFile File { get; set; } // the file sent from the front-end
        
        public class ReturnDTO
        {
            public string Index { get; set; }
            public string FileURL { get; set; }
        }

    }
}

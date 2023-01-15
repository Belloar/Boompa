namespace Boompa.DTO
{
    public class AdminDTO
    {
        public class CreateModel
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }
            public string? Email { get; set; }
        }
        public class UpdateModel
        {
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? PhoneNumber { get; set; }
            public byte[]? ProfilePicture { get; set; }

        }
    }
}

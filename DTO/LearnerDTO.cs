namespace Boompa.DTO
{
    public class LearnerDTO
    {
        public class CreateRequestModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public int Age { get; set; }
        }
    }
}

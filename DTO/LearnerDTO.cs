using Boompa.Entities;

namespace Boompa.DTO
{
    public class LearnerDTO
    {
        public class CreateRequestModel
        {
            public int UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public int Age { get; set; }
            public string SchoolName { get; set; }
            
        }
        public class UpdateRequestModel
        {
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Email { get; set; }
            
        }
        public class UpdateStatsModel
        {
            public int TicketCount { get; set; }
            public int CoinCount { get; set; }
            public Visit Visit { get; set; }
            public Diary Diary { get; set; }

        }
        public class DeleteModel
        {
            public int UserId { get; set; }
            public bool IsDeleted { get; set; }
            public string Deletedby { get; set; }
            public DateTime DeletedOn { get; set; }
        }
        public class DisplayModel
        {
            public int UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int CoinCount { get; set; }
            public int TicketCount { get; set; }
        }
    }
}

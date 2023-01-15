using Boompa.Entities;
using Boompa.Enums;

namespace Boompa.DTO
{
    public class LearnerDTO
    {
        public class CreateRequest
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public int Age { get; set; }
            public string SchoolName { get; set; }
        }
        public class UpdateInfo
        {
            public string ModifierName { get; set; }
            public int UserId { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Email { get; set; }
            public string? School { get; set; }
            
        }
        public class UpdateStats
        {
            public int UserId { get; set; }
            public int DiaryId { get; set; }
            public int TicketCount { get; set; }
            public int CoinCount { get; set; }
            public Visit Visit { get; set; }
            

        }
        public class DeleteModel
        {
            public int UserId { get; set; }
            public bool IsDeleted { get; set; }
            public string Deletedby { get; set; }
            public DateTime DeletedOn { get; set; }
        }
        public class LearnerInfo
        {
            public int UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public bool Status { get; set; }
            public Rank Rank { get; set; }
            public string? School { get; set; }
        }
    }
}

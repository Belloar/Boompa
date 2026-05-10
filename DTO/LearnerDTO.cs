using Boompa.Entities;
using Boompa.Entities.Identity;
//using Boompa.Enums;

namespace Boompa.DTO
{
    public class LearnerDTO
    {
        
        public record CreateLearner()
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            
        }
        public record UpdateInfo()
        {
            public string ModifiedBy { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string School { get; set; }
            public string PhoneNumber { get; set; }
            
        }

        public record ProfilePicUpdateModel
        {
            public IFormFile ProfilePic { get; set; }
        }

        public record UpdateStats
        {
            public Guid CategoryId { get; set; }
            public int TicketCount { get; set; }
            public int CoinCount { get; set; }
            public double Duration { get; set; }
            public DateOnly Date { get; set; }


        }
        public record DeleteModel
        {
            public Guid UserId { get; set; }
            public bool IsDeleted { get; set; }
            public string Deletedby { get; set; }
            public DateTime DeletedOn { get; set; }
        }
        public record LearnerInfo
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string? ProfilePicture { get; set; }
            public bool Status { get; set; }
            public string Rank { get; set; }
            public string? School { get; set; }
            public int CoinCount { get; set; }
            public int TicketCount { get; set; }
            public int ExpPoints { get; set; }
            public ICollection<MaterialDTO.SourceDescriptor> Bookmarks { get; set; }

        }

        public record ReturnLearner
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public int Age { get; set; }
            public string PhoneNumber { get; set; }
        }
    }
}

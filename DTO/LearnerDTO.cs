using Boompa.Entities;
using Boompa.Entities.Identity;
//using Boompa.Enums;

namespace Boompa.DTO
{
    public class LearnerDTO
    {
        public class CreateRequest
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public int Age { get; set; }
            public string PhoneNumber { get; set; }
            
        }
        public class UpdateInfo
        {
            public string ModifierName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string School { get; set; }
            public string PhoneNumber { get; set; }
            
        }
        public class UpdateStats
        {
            public Guid CategoryId { get; set; }
            public int TicketCount { get; set; }
            public int CoinCount { get; set; }
            public double Duration { get; set; }
            public DateOnly Date { get; set; }


        }
        public class DeleteModel
        {
            public Guid UserId { get; set; }
            public bool IsDeleted { get; set; }
            public string Deletedby { get; set; }
            public DateTime DeletedOn { get; set; }
        }
        public class LearnerInfo
        {
            public Guid UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public bool Status { get; set; }
            public string Rank { get; set; }
            public string? School { get; set; }
            public ICollection<Role> Roles { get; set; } = new HashSet<Role>();
        }

        public class ReturnLearner
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

using Boompa.Entities.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Boompa.Auth
{
    public class IdentityDTO
    {
        //public class CreateRequestModel
        //{
        //    public string UserName { get; set; }
        //    public string Password { get; set; }
        //    public string Email { get; set; }
        //    public string PhoneNumber { get; set; }

        //}
        public class UpdateRequestModel
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }
            public string? Email { get; set; }
            
        }
        public class ValidUser
        {
            public Guid UserId { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public ICollection<string>? Roles = new List<string>();
        }
        public class UserLoginModel
        {
            public string SearchString { get; set; }
            public string Password { get; set; }
        }

        public record UsersResponseModel(string UserName, ICollection<string>Roles, string Email, DateTime CreatedOn, bool IsDeleted );
    }
}

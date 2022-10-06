using Boompa.Entities.Identity;

namespace Boompa.Auth
{
    public class IdentityDTO
    {
        public class CreateRequestModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }

        }
        public class UpdateRequestModel
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }
            public string? Email { get; set; }
            public string? PhoneNumber { get; set; }
        }
        public class ValidUserModel
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Token { get; set; }
            public bool IsAuthenticated { get; set; }
            public ICollection<UserRole> Roles { get; set; }
        }
        public class UserLoginModel
        {
            public string CheckString { get; set; }
            public string Password { get; set; }
        }
    }
}

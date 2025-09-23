using Boompa.Auth;
using Boompa.Entities.Base;
using Boompa.Entities.Identity;

namespace Boompa.DTO
{
    public class Response : BaseResponse
    {

        public class LoginResponse 
        {
            public string? JWT { get; set; }
            
        }
        public class ProgressResponse 
        {
            public int Id { get; set; }
        }
            



    }
}

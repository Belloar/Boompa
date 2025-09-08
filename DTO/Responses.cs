using Boompa.Auth;
using Boompa.Entities.Base;
using Boompa.Entities.Identity;

namespace Boompa.DTO
{
    public class Responses
    {

        public class LoginResponse : BaseResponse
        {
            public string? JWT { get; set; }
            public IdentityDTO.ValidUser Data { get; set; }
        }

            



    }
}

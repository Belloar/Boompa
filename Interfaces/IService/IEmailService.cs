using Microsoft.Extensions.Caching.Memory;

namespace Boompa.Interfaces.IService
{
    public interface IEmailService
    {
        
        Task SendVerificationCode(string email, string code);
        Task<bool> VerifyCode(string email, string code);
        Task<string> GenerateCode();
        
    }
}

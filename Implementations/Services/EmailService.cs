using Boompa.Interfaces.IService;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Net.Mail;

namespace Boompa.Implementations.Services
{
    public class EmailService : IEmailService
    {
        private readonly IMemoryCache _cache;

        public EmailService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<string> GenerateCode()
        {
            var random = new Random();
            var code = random.Next(100000, 999999);
            return code.ToString();
        }

        public async Task SendVerificationCode(string recepientEmail, string verificationCode)
        {
            var mail = new MailMessage()
            {
                From = new MailAddress("rahmanbello2018@gmail.com"),
                Subject = "Email Verification",
                Body = $"To complete your registration on Boompa, we need to verify that the email provided is valid and functional Your verification code is {verificationCode} ",
            };
            mail.To.Add(recepientEmail);

            var smtpClient = new SmtpClient("sandbox.smtp.mailtrap.io", 587)
            {
                Credentials = new NetworkCredential("BoompaTestEnv","BoompaTestEnv123"),
                EnableSsl = true
            };

            try
            {
                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("email could not be sent");
            }

            _cache.Set(recepientEmail,verificationCode,TimeSpan.FromMinutes(3));
        }

        public async Task<bool> VerifyCode(string email, string code)
        {
            if (!_cache.TryGetValue(email, out string storedCode))
                return false;

            return  storedCode.Equals(code);
        }
    }
}


using Boompa.DTO;
using Boompa.Entities.Identity;

namespace Boompa.Auth
{
    public interface IIdentityService 
    {
        
        Task UpdateAsync(Guid id, IdentityDTO.UpdateRequestModel model);
        Task<Response> GetUsersAsync(int numberOfRecordsToSkip);
        Task<bool> CheckUser(string email);
        Task<User> GetUserAsync(Guid id);
        Task<User> GetUserAsync(string checkString);
        Task DeleteAsync(Guid id);
        Task<string> GenerateToken(IdentityDTO.ValidUser model);
        Task<IdentityDTO.ValidUser> AuthenticateUser(string username, string password,string page);
        Task AddRoleAsync(string role);
        Task SendEmail(string email);
        Task<Response> VerifyEmail(string email,string code);
        Task  UpdateUserRole(Guid id,string role);
        Task<Response> UserLogin(string username, string password, string page);
        

    }
}

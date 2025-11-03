
using Boompa.DTO;
using Boompa.Entities.Identity;

namespace Boompa.Auth
{
    public interface IIdentityService 
    {
        
        Task UpdateAsync(int id, IdentityDTO.UpdateRequestModel model);
        Task<Response> GetUsersAsync();
        Task<bool> CheckUser(string email);
        Task<User> GetUserAsync(int id);
        Task<User> GetUserAsync(string checkString);
        Task DeleteAsync(int id);
        Task<string> GenerateToken(IdentityDTO.ValidUser model);
        Task<IdentityDTO.ValidUser> AuthenticateUser(string username, string password);
        Task AddRoleAsync(string role);
        Task  UpdateUserRole(int id,string role);
        

    }
}


using Boompa.Entities.Identity;

namespace Boompa.Auth
{
    public interface IIdentityService 
    {
        
        Task<int> UpdateAsync(int id, IdentityDTO.UpdateRequestModel model, CancellationToken cancellationToken);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<bool> CheckUser(string email);
        Task<User> GetUserAsync(int id);
        Task<User> GetUserAsync(string checkString);
        Task<int> DeleteAsync(int id, CancellationToken cancellationToken);
        
        Task<string> GenerateToken(IdentityDTO.ValidUser model);
        Task<IdentityDTO.ValidUser> AuthenticateUser(string username, string password);
        Task<int>AddRoleAsync(string role, CancellationToken cancellationToken);
        Task<int> UpdateUserRole(int id,string role, CancellationToken cancellationToken);
        

    }
}

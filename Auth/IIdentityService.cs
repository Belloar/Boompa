
using Boompa.Entities.Identity;

namespace Boompa.Auth
{
    public interface IIdentityService 
    {
        
        Task<int> UpdateAsync(int id, IdentityDTO.UpdateRequestModel model, CancellationToken cancellationToken);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        Task<User> GetUserAsync(string checkString);
        Task<int> DeleteAsync(int id, CancellationToken cancellationToken);
        bool CheckUser(string email);
        Task<IdentityDTO.ValidUserModel> AuthenticateUser(string email);
        Task<string> GenerateToken(User model);
        Task<User> AuthenticateUser(string username, string password);
        Task<int>AddRoleAsync(string role, CancellationToken cancellationToken);
        Task<int> UpdateUserRole(int id,string role, CancellationToken cancellationToken);
        

    }
}

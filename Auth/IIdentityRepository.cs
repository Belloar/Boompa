using Boompa.Entities.Identity;
using Boompa.DTO;

namespace Boompa.Auth
{
    public interface IIdentityRepository
    {
        Task<int> CreateAsync(User user, CancellationToken cancellationToken);
        Task<bool> CheckUser(string email);
        Task<int> UpdateAsync(int id, User user, CancellationToken cancellationToken);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        Task<User> GetUserAsync(string searchString, bool isEmail = false);
        
        Task<int> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<Role> AddRoleAsync(string role,CancellationToken cancellationToken);
        Task<Role> GetRoleAsync(string role);
        Task<IEnumerable<string>> GetRoleAsync(int roleId);
        Task<int> UpdateUserRole(int id, Role role, CancellationToken cancellationToken);
        Task<int> AddUserRole(IEnumerable<UserRole> role, CancellationToken cancellationToken);


    }
}

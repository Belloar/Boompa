using Boompa.Entities.Identity;
using Boompa.DTO;

namespace Boompa.Auth
{
    public interface IIdentityRepository
    {
        Task CreateAsync(User user);
        Task<bool> CheckUser(string email);
        Task UpdateAsync(Guid id, User user);
        Task<IEnumerable<User>> GetUsersAsync(int numberOfRecordsToSkip);
        Task<User> GetUserAsync(Guid id);
        Task<User> GetUserAsync(string searchString, bool isEmail = false);
        
        Task DeleteAsync(Guid id);
        Task<Role> AddRoleAsync(string role);
        Task<Role> GetRoleAsync(string role);
        Task<IEnumerable<string>> GetRolesAsync();
       
        Task UpdateUserRole(Guid id, Role role);
        Task AddUserRoles(ICollection<UserRole> role);


    }
}

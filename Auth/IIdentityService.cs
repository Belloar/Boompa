
using Boompa.Entities.Identity;

namespace Boompa.Auth
{
    public interface IIdentityService
    {
        Task<int> CreateAsync(IdentityDTO.CreateRequestModel model, CancellationToken cancellationToken);
        Task<int> UpdateAsync(int id, IdentityDTO.UpdateRequestModel model, CancellationToken cancellationToken);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        Task<User> GetUserAsync(string checkString);
        Task<int> DeleteAsync(int id, CancellationToken cancellationToken);
        bool CheckEmail(string email);
        Task<IdentityDTO.ValidUserModel> ValidateUser(string email);
        Task<string> GenerateToken(User model);
        User AuthenticateUser(string username, string password);
        Task<int>AddRoleAsync(string role, CancellationToken cancellationToken);

    }
}

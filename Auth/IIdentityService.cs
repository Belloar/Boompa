
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
        Task<IdentityDTO.ValidatedUserModel> ValidateUser(string email);
        string GenerateToken(IdentityDTO.ValidatedUserModel model);
        User AuthenticateUser(string username, string password);

    }
}

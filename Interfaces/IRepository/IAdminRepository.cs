using Boompa.DTO;
using Boompa.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Boompa.Interfaces.IRepository
{
    public interface IAdminRepository
    {
        Task<int> AddAdminAsync(Administrator admin, CancellationToken cancellationToken);
        Task<int> DeleteAdminAsync(int id);
        Task<IEnumerable<Administrator>> GetAdminsAsync();
        Task<Administrator> GetAdminAsync(int id);
        Task<Administrator> GetAdminAsync(string checkString);
        Task<int> UpdateAdminAsync(AdminDTO.UpdateModel requestModel, CancellationToken cancellationToken);

    }
}

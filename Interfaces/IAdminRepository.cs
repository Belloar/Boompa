using Boompa.DTO;
using Boompa.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Boompa.Interfaces
{
    public interface IAdminRepository
    {
        Task<bool> CreateAdminAsync(AdminDTO.CreateRequestModel requestModel,CancellationToken cancellationToken);
        Task<bool> UpdateAdminAsync(AdminDTO.CreateRequestModel requestModel, CancellationToken cancellationToken);
        Task<IEnumerable<Administrator>> GetAdminsAsync();
        Task<Administrator> GetAdminAsync(int id = 0);
        Task<Administrator> GetAdminAsync(string checkString);

    }
}

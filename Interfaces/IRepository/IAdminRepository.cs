using Boompa.DTO;
using Boompa.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Boompa.Interfaces.IRepository
{
    public interface IAdminRepository
    {
        Task AddAdminAsync(Admin admin);
        Task<int> DeleteAdminAsync(int id);
        Task<IEnumerable<Admin>> GetAdminsAsync();
        Task<Admin> GetAdminAsync(int id);
        Task<Admin> GetAdminAsync(string checkString);
        Task<int> UpdateAdminAsync(AdminDTO.UpdateModel requestModel);

    }
}

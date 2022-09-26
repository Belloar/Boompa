using Boompa.DTO;
using Boompa.Entities;
using Boompa.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Boompa.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        public Task<bool> CreateAdminAsync(AdminDTO.CreateRequestModel requestModel, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Administrator> GetAdminAsync(int id = 0)
        {
            throw new NotImplementedException();
        }

        public Task<Administrator> GetAdminAsync(string checkString)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Administrator>> GetAdminsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAdminAsync(AdminDTO.CreateRequestModel requestModel, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

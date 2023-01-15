using Boompa.Context;
using Boompa.DTO;
using Boompa.Entities;
using Boompa.Interfaces.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Boompa.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationContext _context;
        public AdminRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<int> AddAdminAsync(Administrator model, CancellationToken cancellationToken)
        {
            await _context.Administrators.AddAsync(model);
            var result = await _context.SaveChangesAsync(cancellationToken);
            if(result == 0) return 0;
            return result;
        }

        public Task<int> DeleteAdminAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Administrator> GetAdminAsync(int id)
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

        public Task<int> UpdateAdminAsync(AdminDTO.UpdateModel requestModel, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

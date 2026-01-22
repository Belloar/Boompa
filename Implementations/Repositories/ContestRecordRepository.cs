using Boompa.Context;
using Boompa.DTO;
using Boompa.Entities;
using Boompa.Interfaces.IRepository;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace Boompa.Implementations.Repositories
{
    public class ContestRecordRepository : IContestRecordRepository
    {

        private readonly BoompaContext _context;

        public ContestRecordRepository(BoompaContext context)
        {
            _context = context;
        }
        public async Task AddRecord(ContestRecord record)
        {
            await _context.ContestRecords.AddAsync(record);

        }

        public async Task<ICollection<ContestRecord>> GetAllRecords()
        {
            return await _context.ContestRecords.ToListAsync();
        }

        public async Task<ICollection<ContestRecord>> GetRecordsByMonth(DateOnly date)
        {
            return await _context.ContestRecords.Where(cr => cr.Date.Month == date.Month && cr.Date.Year == date.Year).ToListAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Data;
using SY.OnlineApp.Repos.Repositories.Interfaces;

namespace SY.OnlineApp.Repos.Repositories
{
    public class BusinessRepo : IBusinessRepo
    {
        private readonly BusinessDbContext _context;

        public BusinessRepo(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<BusinessData> entries)
        {
            await _context.BusinessEntries.AddRangeAsync(entries);
            await _context.SaveChangesAsync();
        }

        public async Task ClearAsync()
        {
            var all = await _context.BusinessEntries.ToListAsync();
            _context.BusinessEntries.RemoveRange(all);
            await _context.SaveChangesAsync();
        }

        public async Task<List<BusinessData>> GetAllAsync()
        {
            return await _context.BusinessEntries.ToListAsync();
        }

        public async Task<BusinessData> FindByNameAsync(string name)
        {
            return await _context.BusinessEntries.FirstOrDefaultAsync(e => e.Name == name);
        }

        public async Task AddAsync(BusinessData entry)
        {
            await _context.BusinessEntries.AddAsync(entry);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SY.OnlineApp.Data;
using SY.OnlineApp.Data.Entities;

namespace SY.OnlineApp.Repos.Repositories
{
    public class LiabilityRepo : ILiabilityRepo
    {
        private readonly BusinessDbContext _context;

        public LiabilityRepo(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<Liability> entries)
        {
            await _context.Liabilities.AddRangeAsync(entries);
            await _context.SaveChangesAsync();
        }

        public async Task ClearAsync()
        {
            _context.Liabilities.RemoveRange(_context.Liabilities);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Liability>> GetAllAsync()
        {
            return await _context.Liabilities.ToListAsync();
        }

        public async Task<Liability?> FindByNameAsync(string name)
        {
            return await _context.Liabilities.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task AddAsync(Liability entry)
        {
            await _context.Liabilities.AddAsync(entry);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

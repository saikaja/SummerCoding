using Microsoft.EntityFrameworkCore;
using NIA.OnlineApp.Data.Entities;

namespace NIA.OnlineApp.Data.Repositories
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
    }

}

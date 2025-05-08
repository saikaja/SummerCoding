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
    }

}

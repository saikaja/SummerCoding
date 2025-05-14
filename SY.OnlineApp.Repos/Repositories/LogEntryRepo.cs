using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SY.OnlineApp.Data;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Repos.Repositories.Interfaces;

namespace SY.OnlineApp.Repos.Repositories
{
    public class LogEntryRepo : ILogEntryRepo
    {
        private readonly BusinessDbContext _context;

        public LogEntryRepo(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(LogEntry log)
        {
            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LogEntry>> GetAllAsync()
        {
            return await _context.Logs.ToListAsync();
        }
    }
}

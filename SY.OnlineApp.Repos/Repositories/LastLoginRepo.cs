using SY.OnlineApp.Data;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SY.OnlineApp.Repos.Repositories
{
    public class LastLoginRepo : ILastLoginRepo
    {
        private readonly BusinessDbContext _context;

        public LastLoginRepo(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(LastLogin login)
        {
            await _context.Set<LastLogin>().AddAsync(login);
            await _context.SaveChangesAsync();
        }

        public async Task<LastLogin?> GetLastLoginAsync(int registrationId)
        {
            return await _context.Set<LastLogin>()
                .OrderByDescending(l => l.LoginTimestamp)
                .FirstOrDefaultAsync(l => l.RegistrationId == registrationId);
        }
    }
}

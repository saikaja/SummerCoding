using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SY.OnlineApp.Data;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Repos.Repositories.Interfaces;

namespace SY.OnlineApp.Repos.Repositories
{
    public class LastLoginRepo : ILastLoginRepo
    {
        private readonly BusinessDbContext _context;

        public LastLoginRepo(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task AddOrUpdateAsync(int registrationId)
        {
            var existingLogin = await _context.LastLogins
                .FirstOrDefaultAsync(l => l.RegistrationId == registrationId);

            if (existingLogin != null)
            {
                existingLogin.LoginTimestamp = DateTime.UtcNow;
                _context.LastLogins.Update(existingLogin);
            }
            else
            {
                var newLogin = new LastLogin
                {
                    RegistrationId = registrationId,
                    LoginTimestamp = DateTime.UtcNow
                };
                await _context.LastLogins.AddAsync(newLogin);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LastLogin login)
        {
            _context.LastLogins.Update(login);
            await _context.SaveChangesAsync();
        }

        public async Task<LastLogin?> GetLastLoginAsync(int registrationId)
        {
            return await _context.LastLogins
                .FirstOrDefaultAsync(l => l.RegistrationId == registrationId);
        }
    }
}

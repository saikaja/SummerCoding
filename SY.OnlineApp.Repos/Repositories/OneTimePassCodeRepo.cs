using Microsoft.EntityFrameworkCore;
using SY.OnlineApp.Data;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace SY.OnlineApp.Repos.Repositories
{
    public class OneTimePassCodeRepo : IOneTimePassCodeRepo
    {
        private readonly BusinessDbContext _context;

        public OneTimePassCodeRepo(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OneTimePassCode code)
        {
            await _context.Set<OneTimePassCode>().AddAsync(code);
            await _context.SaveChangesAsync();
        }

        public async Task<OneTimePassCode?> GetValidCodeAsync(int registrationId, string code)
        {
            return await _context.Set<OneTimePassCode>()
                .FirstOrDefaultAsync(o =>
                    o.RegistrationId == registrationId &&
                    o.OneCode == code &&
                    o.ExpirationTime > DateTime.UtcNow);
        }
    }
}

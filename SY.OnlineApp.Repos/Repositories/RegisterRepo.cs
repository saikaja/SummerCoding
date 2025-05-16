using SY.OnlineApp.Data;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SY.OnlineApp.Repos.Repositories
{
    public class RegisterRepo : IRegisterRepo
    {
        private readonly BusinessDbContext _context;

        public RegisterRepo(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Register register)
        {
            await _context.Set<Register>().AddAsync(register);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserNameExistsAsync(string username)
        {
            return await _context.Set<Register>().AnyAsync(r => r.UserName == username);
        }
    }
}

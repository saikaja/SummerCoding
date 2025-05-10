using Microsoft.EntityFrameworkCore;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SY.OnlineApp.Repos.Repositories
{
    public class TypeUtilRepo : ITypeUtilRepo
    {
        private readonly AppDbContext _context;

        public TypeUtilRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TypeUtil>> GetAllAsync()
        {
            return await _context.Types.ToListAsync();
        }

        public async Task<TypeUtil?> GetByIdAsync(int id)
        {
            return await _context.Types.FindAsync(id);
        }

        public async Task AddAsync(TypeUtil typeUtil)
        {
            await _context.Types.AddAsync(typeUtil);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TypeUtil typeUtil)
        {
            _context.Types.Update(typeUtil);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TypeUtil typeUtil)
        {
            _context.Types.Remove(typeUtil);
            await _context.SaveChangesAsync();
        }
    }
}

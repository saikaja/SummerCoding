using Microsoft.EntityFrameworkCore;
using NIA.OnlineApp.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NIA.OnlineApp.Data.Repositories
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

        public async Task AddAsync(int id, TypeUtil typeUtil)
        {
            await _context.Types.AddAsync(typeUtil);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, TypeUtil typeUtil)
        {
            _context.Types.Update(typeUtil);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, TypeUtil typeUtil)
        {
            var entity = await _context.Types.FindAsync(id);
            if (entity != null)
            {
                _context.Types.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

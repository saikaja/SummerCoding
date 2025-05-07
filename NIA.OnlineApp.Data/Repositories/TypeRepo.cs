using Microsoft.EntityFrameworkCore;
using NIA.OnlineApp.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NIA.OnlineApp.Data.Repositories
{
    public class TypeRepo : ITypeRepo
    {
        private readonly AppDbContext _context;

        public TypeRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TypeInformation>> GetByTypeIdAsync(int typeId)
        {
            return await _context.TypeInformations
                                 .Where(ti => ti.Type_Id == typeId)
                                 .ToListAsync();
        }

        public async Task AddRangeAsync(IEnumerable<TypeInformation> entries)
        {
            await _context.TypeInformations.AddRangeAsync(entries);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TypeInformation entry)
        {
            _context.TypeInformations.Update(entry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.TypeInformations.FindAsync(id);
            if (entity != null)
            {
                _context.TypeInformations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

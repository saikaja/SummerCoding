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
            if (typeId <= 0)
                throw new ArgumentException("Type ID must be a positive integer.", nameof(typeId));
           
            var results = await _context.TypeInformations
                                 .Where(ti => ti.Type_Id == typeId)
                                 .ToListAsync();

            if (results == null || !results.Any())
                throw new InvalidOperationException("No entries found for TypeId = {typeId}");

            return results;
        }

        public async Task AddRangeAsync(IEnumerable<TypeInformation> entries)
        {
            if (entries == null || !entries.Any())
                throw new ArgumentException("Entries cannot be null or empty.", nameof(entries));

            await _context.TypeInformations.AddRangeAsync(entries);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id,TypeInformation entry)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be a positive integer.", nameof(id));

            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            if (entry.Id != id)
                throw new InvalidOperationException("Mismatched ID: entry.Id does not match the provided id.");

            var existing = await _context.TypeInformations.FindAsync(id);
            if (existing == null)
                throw new InvalidOperationException($"No entry found to update with Id = {id}");

            _context.TypeInformations.Update(entry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, TypeInformation entry)
        {
            var entity = await _context.TypeInformations.FindAsync(id);
            if (entity != null)
            {
                if (id <= 0)
                    throw new ArgumentException("Id must be a positive integer.", nameof(id));

                if (entry == null)
                    throw new ArgumentNullException(nameof(entry));

                if (entry.Id != id)
                    throw new InvalidOperationException("Mismatched ID: entry.Id does not match the provided id.");

                var results = await _context.TypeInformations.FindAsync(id);
                if (results == null)
                    throw new InvalidOperationException($"No entry found to delete with Id = {id}");

                _context.TypeInformations.Remove(entry);
                await _context.SaveChangesAsync();
            }
        }
    }
}

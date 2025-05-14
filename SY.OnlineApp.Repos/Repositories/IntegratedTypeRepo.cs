using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Data;
using SY.OnlineApp.Repos.Repositories.Interfaces;

namespace SY.OnlineApp.Repos.Repositories
{
    public class IntegratedTypeRepo : IIntegratedTypeRepo
    {
        private readonly BusinessDbContext _context;

        public IntegratedTypeRepo(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IntegratedType>> GetAllAsync()
        {
            return await _context.IntegratedTypes.ToListAsync();
        }

        public async Task<IntegratedType?> GetByIdAsync(int id)
        {
            return await _context.IntegratedTypes.FindAsync(id);
        }

        public async Task AddAsync(IntegratedType type)
        {
            _context.IntegratedTypes.Add(type);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string type)
        {
            return await _context.IntegratedTypes.AnyAsync(t => t.Type == type);
        }
    }

}

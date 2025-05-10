using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Repos.Repositories;

namespace SY.OnlineApp.Data.Repositories
{
    public class IntegratedStatusRepo : IIntegratedStatusRepo
    {
        private readonly BusinessDbContext _context;

        public IntegratedStatusRepo(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task<IntegratedStatus?> GetByIntegratedTypeAsync(int integratedTypeId)
        {
            return await _context.IntegratedStatuses
                .Include(s => s.IntegratedType)
                .FirstOrDefaultAsync(s => s.IntegratedId == integratedTypeId);
        }

        public async Task<bool> SetIntegrationStatusAsync(int integratedTypeId, bool isIntegrated)
        {
            var status = await GetByIntegratedTypeAsync(integratedTypeId);
            if (status == null) return false;

            status.IsDataIntegrated = isIntegrated;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

}

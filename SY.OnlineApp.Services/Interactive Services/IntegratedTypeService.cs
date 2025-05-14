using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Repos.Repositories.Interfaces;

namespace SY.OnlineApp.Services.Integrated_Type_Services
{
    public class IntegratedTypeService : IIntegratedTypeService
    {
        private readonly IIntegratedTypeRepo _repo;

        public IntegratedTypeService(IIntegratedTypeRepo repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<IntegratedType>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<IntegratedType?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<bool> CreateTypeAsync(IntegratedType type)
        {
            if (await _repo.ExistsAsync(type.Type))
                return false;

            await _repo.AddAsync(type);
            return true;
        }
    }

}

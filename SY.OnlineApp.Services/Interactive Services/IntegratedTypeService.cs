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
            try
            {
                return await _repo.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving types.", ex);
            }
        }

        public async Task<IntegratedType?> GetByIdAsync(int id)
        {
            try
            {
                return await _repo.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving type by ID.", ex);
            }
        }

        public async Task<bool> CreateTypeAsync(IntegratedType type)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(type.Type))
                    throw new ArgumentException("Type is required.");

                if (await _repo.ExistsAsync(type.Type))
                    return false;

                await _repo.AddAsync(type);
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error creating type.", ex);
            }
        }
    }
}


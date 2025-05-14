using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace SY.OnlineApp.Services.Integrated_Type_Services
{
    public class IntegratedTypeService : IIntegratedTypeService
    {
        private readonly IIntegratedTypeRepo _repo;
        private readonly ILogger<IntegratedTypeService> _logger;

        public IntegratedTypeService(IIntegratedTypeRepo repo, ILogger<IntegratedTypeService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<IEnumerable<IntegratedType>> GetAllAsync()
        {
            try
            {
                return await _repo.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all integrated types.");
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
                _logger.LogError(ex, "Error occurred while retrieving integrated type with ID {Id}.", id);
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
                _logger.LogError(ex, "Error occurred while creating integrated type: {Type}.", type?.Type);
                throw new ApplicationException("Error creating type.", ex);
            }
        }
    }
}

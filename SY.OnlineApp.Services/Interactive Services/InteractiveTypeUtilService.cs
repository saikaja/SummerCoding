using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace SY.OnlineApp.Services.InteractiveServices
{
    public class InteractiveTypeUtilService : IInteractiveTypeUtilService
    {
        private readonly ITypeUtilRepo _repository;
        private readonly ILogger<InteractiveTypeUtilService> _logger;

        public InteractiveTypeUtilService(ITypeUtilRepo repository, ILogger<InteractiveTypeUtilService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<TypeUtil>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all TypeUtil records.");
                throw new ApplicationException("Error retrieving all TypeUtil entries.", ex);
            }
        }

        public async Task<TypeUtil?> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving TypeUtil with ID {Id}.", id);
                throw new ApplicationException($"Error retrieving TypeUtil by ID {id}.", ex);
            }
        }

        public async Task<bool> AddAsync(TypeUtil typeUtil)
        {
            try
            {
                await _repository.AddAsync(typeUtil);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting TypeUtil with ID {Id}.", typeUtil?.Id);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TypeUtil typeUtil)
        {
            try
            {
                await _repository.UpdateAsync(typeUtil);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating TypeUtil with ID {Id}.", typeUtil?.Id);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(TypeUtil typeUtil)
        {
            try
            {
                await _repository.DeleteAsync(typeUtil);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting TypeUtil with ID {Id}.", typeUtil?.Id);
                return false;
            }
        }
    }
}

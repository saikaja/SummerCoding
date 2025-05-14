using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace SY.OnlineApp.Services.InteractiveServices
{
    public class InteractiveTypeInformationService : IInteractiveITypeInformationService
    {
        private readonly ITypeInformationRepo _typeRepo;
        private readonly ILogger<InteractiveTypeInformationService> _logger;

        public InteractiveTypeInformationService(ITypeInformationRepo typeRepo, ILogger<InteractiveTypeInformationService> logger)
        {
            _typeRepo = typeRepo;
            _logger = logger;
        }

        public async Task<IEnumerable<TypeInformation>> GetByTypeIdAsync(int Type_Id)
        {
            try
            {
                return await _typeRepo.GetByTypeIdAsync(Type_Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving TypeInformation by Type_Id {TypeId}.", Type_Id);
                throw new ApplicationException("Error retrieving TypeInformation by type ID.", ex);
            }
        }

        public async Task<IEnumerable<TypeInformation>> GetAllAsync()
        {
            try
            {
                return await _typeRepo.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all TypeInformation records.");
                throw new ApplicationException("Error retrieving all TypeInformation records.", ex);
            }
        }

        public async Task<bool> InsertMultipleAsync(IEnumerable<TypeInformation> list)
        {
            try
            {
                await _typeRepo.AddRangeAsync(list);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting multiple TypeInformation entries.");
                throw new ApplicationException("Insert failed.", ex);
            }
        }

        public async Task<bool> UpdateAttributesAsync(int Type_Id, TypeInformation typeInformation)
        {
            try
            {
                await _typeRepo.UpdateAsync(Type_Id, typeInformation);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating TypeInformation for Type_Id {TypeId}.", Type_Id);
                return false;
            }
        }

        public async Task<bool> DeleteAttributesAsync(int Type_Id, TypeInformation typeInformation)
        {
            try
            {
                await _typeRepo.DeleteAsync(Type_Id, typeInformation);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting TypeInformation for Type_Id {TypeId}.", Type_Id);
                return false;
            }
        }
    }
}

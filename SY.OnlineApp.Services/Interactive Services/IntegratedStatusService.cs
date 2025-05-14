using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace SY.OnlineApp.Services.Integrated_Status_Services
{
    public class IntegratedStatusService : IIntegratedStatusService
    {
        private readonly IIntegratedStatusRepo _statusRepo;
        private readonly ILogger<IntegratedStatusService> _logger;

        public IntegratedStatusService(IIntegratedStatusRepo statusRepo, ILogger<IntegratedStatusService> logger)
        {
            _statusRepo = statusRepo;
            _logger = logger;
        }

        public async Task<IntegratedStatus> GetStatusByTypeAsync(int integratedTypeId)
        {
            try
            {
                var status = await _statusRepo.GetByIntegratedTypeAsync(integratedTypeId);

                if (status == null)
                    throw new KeyNotFoundException("Status not found.");

                return status;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving status for IntegratedTypeId {IntegratedTypeId}.", integratedTypeId);
                throw new ApplicationException("Error retrieving status.", ex);
            }
        }
        public async Task UpdateStatusAsync(int integratedTypeId, bool isIntegrated)
        {
            try
            {
                var updated = await _statusRepo.SetIntegrationStatusAsync(integratedTypeId, isIntegrated);

                if (!updated)
                    throw new KeyNotFoundException("Status update failed or record not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating status for IntegratedTypeId {IntegratedTypeId} with value {IsIntegrated}.", integratedTypeId, isIntegrated);
                throw new ApplicationException("Error updating status.", ex);
            }
        }

        public async Task CreateStatusAsync(IntegratedStatus status)
        {
            try
            {
                if (status == null || status.IntegratedId <= 0)
                    throw new ArgumentException("Invalid status data.");

                await _statusRepo.AddStatusAsync(status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating status for IntegratedId {IntegratedId}.", status?.IntegratedId);
                throw new ApplicationException("Error creating status.", ex);
            }
        }
    }
}

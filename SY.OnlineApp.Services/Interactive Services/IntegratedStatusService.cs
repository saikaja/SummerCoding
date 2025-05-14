using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Repos.Repositories.Interfaces;

namespace SY.OnlineApp.Services.Integrated_Status_Services
{
    public class IntegratedStatusService : IIntegratedStatusService
    {
        private readonly IIntegratedStatusRepo _statusRepo;

        public IntegratedStatusService(IIntegratedStatusRepo statusRepo)
        {
            _statusRepo = statusRepo;
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
                throw new ApplicationException("Error creating status.", ex);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Repos.Repositories;

namespace SY.OnlineApp.Services.Integrated_Status_Services
{
    public class IntegratedStatusService : IIntegratedStatusService
    {
        private readonly IIntegratedStatusRepo _statusRepo;

        public IntegratedStatusService(IIntegratedStatusRepo statusRepo)
        {
            _statusRepo = statusRepo;
        }

        public async Task<IntegratedStatus?> GetStatusByTypeAsync(int integratedTypeId)
        {
            return await _statusRepo.GetByIntegratedTypeAsync(integratedTypeId);
        }

        public async Task<bool> UpdateIntegrationStatusAsync(int integratedTypeId, bool isIntegrated)
        {
            return await _statusRepo.SetIntegrationStatusAsync(integratedTypeId, isIntegrated);
        }

        public async Task AddStatusAsync(IntegratedStatus status)
        {
            await _statusRepo.AddStatusAsync(status);
        }

    }
}

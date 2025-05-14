using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SY.OnlineApp.Data.Entities;

namespace SY.OnlineApp.Repos.Repositories.Interfaces
{
    public interface IIntegratedStatusRepo
    {
        Task<IntegratedStatus?> GetByIntegratedTypeAsync(int integratedTypeId);
        Task<bool> SetIntegrationStatusAsync(int integratedTypeId, bool isIntegrated);
        Task AddStatusAsync(IntegratedStatus status);
    }
}

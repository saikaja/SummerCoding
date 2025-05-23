﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SY.OnlineApp.Data.Entities;

namespace SY.OnlineApp.Services.Integrated_Status_Services
{
    public interface IIntegratedStatusService
    {
        Task<IntegratedStatus> GetStatusByTypeAsync(int integratedTypeId);
        Task UpdateStatusAsync(int integratedTypeId, bool isIntegrated);
        Task CreateStatusAsync(IntegratedStatus status);
    }
}

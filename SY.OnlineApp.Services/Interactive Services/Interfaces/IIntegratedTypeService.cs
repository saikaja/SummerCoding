using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SY.OnlineApp.Data.Entities;

namespace SY.OnlineApp.Services.Integrated_Type_Services
{
    public interface IIntegratedTypeService
    {
        Task<IEnumerable<IntegratedType>> GetAllAsync();
        Task<IntegratedType?> GetByIdAsync(int id);
        Task<bool> CreateTypeAsync(IntegratedType type);
    }

}

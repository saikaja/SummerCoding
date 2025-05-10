using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using SY.OnlineApp.Data.Entities;

namespace SY.OnlineApp.Repos.Repositories
{
    public interface ITypeInformationRepo
    {
        Task<IEnumerable<TypeInformation>> GetByTypeIdAsync(int typeId);
        Task<IEnumerable<TypeInformation>> GetAllAsync();
        Task AddRangeAsync(IEnumerable<TypeInformation> entries);
        Task UpdateAsync(int typeId, TypeInformation entry);
        Task DeleteAsync(int typeId, TypeInformation entry);
    }


}

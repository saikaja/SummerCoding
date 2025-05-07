using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using NIA.OnlineApp.Data.Entities;

namespace NIA.OnlineApp.Data.Repositories
{
    public interface ITypeInformationRepo
    {
        Task<IEnumerable<TypeInformation>> GetByTypeIdAsync(int typeId);
        Task AddRangeAsync(IEnumerable<TypeInformation> entries);
        Task UpdateAsync(int typeId, TypeInformation entry);
        Task DeleteAsync(int typeId, TypeInformation entry);
    }


}

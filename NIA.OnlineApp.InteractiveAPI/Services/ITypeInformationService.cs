using System.ComponentModel;
using NIA.OnlineApp.Data.Entities;

namespace NIA.OnlineApp.InteractiveAPI.Services
{
    public interface ITypeInformationService
    {
        Task<IEnumerable<TypeInformation>> GetByTypeIdAsync(int Type_Id);
        Task<IEnumerable<TypeInformation>> GetAllAsync();
        Task<bool> InsertMultipleAsync(IEnumerable<TypeInformation> typeInformationList);
        Task<bool> UpdateAttributesAsync(int Type_Id, TypeInformation typeInformation);
        Task<bool> DeleteAttributesAsync(int Type_Id, TypeInformation typeInformation);
    }

}

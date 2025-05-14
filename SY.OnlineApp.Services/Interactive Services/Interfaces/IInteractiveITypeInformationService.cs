using System.ComponentModel;
using SY.OnlineApp.Data.Entities;

namespace SY.OnlineApp.Services.InteractiveServices
{
    public interface IInteractiveITypeInformationService
    {
        Task<IEnumerable<TypeInformation>> GetByTypeIdAsync(int Type_Id);
        Task<IEnumerable<TypeInformation>> GetAllAsync();
        Task<bool> InsertMultipleAsync(IEnumerable<TypeInformation> typeInformationList);
        Task<bool> UpdateAttributesAsync(int Type_Id, TypeInformation typeInformation);
        Task<bool> DeleteAttributesAsync(int Type_Id, TypeInformation typeInformation);
    }

}

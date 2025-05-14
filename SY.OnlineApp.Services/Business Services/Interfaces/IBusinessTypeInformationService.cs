using SY.OnlineApp.Models.Models;

namespace SY.OnlineApp.Services.BusinessServices

{
    public interface IBusinessTypeInformationService
    {
        Task<List<TypeInformationDto>> FetchTypeInformationsAsync();
        Task<List<TypeInformationDto>> FetchAndCombineTypesAsync();
        Task<List<TypeInformationDto>> FetchStoreAndReturnTypeInformationsAsync();
    }
}

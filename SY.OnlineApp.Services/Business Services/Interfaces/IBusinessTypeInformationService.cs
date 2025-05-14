using SY.OnlineApp.Models.Models;

namespace SY.OnlineApp.Services.BusinessServices

{
    public interface IBusinessTypeInformationService
    {
        Task<List<TypeInformationDto>> GetTypeInformationsAsync();
    }
}

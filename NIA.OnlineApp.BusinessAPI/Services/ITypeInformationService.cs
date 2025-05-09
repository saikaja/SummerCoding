using SY.OnlineApp.BusinessAPI.Models;

namespace SY.OnlineApp.BusinessAPI.Services

{
    public interface ITypeInformationService
    {
        Task<List<TypeInformationDto>> GetTypeInformationsAsync();
    }
}

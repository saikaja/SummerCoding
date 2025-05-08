using NIA.OnlineApp.BusinessAPI.Models;

namespace NIA.OnlineApp.BusinessAPI.Services

{
    public interface ITypeInformationService
    {
        Task<List<TypeInformationDto>> GetTypeInformationsAsync();
    }
}

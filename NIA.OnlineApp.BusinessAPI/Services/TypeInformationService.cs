using NIA.OnlineApp.BusinessAPI.Models;

namespace NIA.OnlineApp.BusinessAPI.Services
{
    public class TypeInformationService : ITypeInformationService
    {
        private readonly HttpClient _httpClient;

        public TypeInformationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TypeInformationDto>> GetTypeInformationsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<TypeInformationDto>>("api/TypeInformations");
            return response ?? new List<TypeInformationDto>();
        }
    }
}

using SY.OnlineApp.Models.Models;
using System.Net.Http.Json;

namespace SY.OnlineApp.Services.BusinessServices
{
    public class BusinessTypeInformationService : IBusinessTypeInformationService
    {
        private readonly HttpClient _httpClient;

        public BusinessTypeInformationService(HttpClient httpClient)
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

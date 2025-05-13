using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using SY.OnlineApp.Models.Models;

namespace SY.OnlineApp.Services.Business_Services
{
    public class LiabilityService : ILiabilityService
    {
        private readonly HttpClient _httpClient;

        public LiabilityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TypeInformationDto>> GetLiabilitiesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<TypeInformationDto>>("api/TypeInformations/type/3");
            return response ?? new List<TypeInformationDto>();
        }
    }
}

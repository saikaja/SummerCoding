using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Models.Models;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using System.Net.Http.Json;

namespace SY.OnlineApp.Services.BusinessServices
{
    public class BusinessTypeInformationService : IBusinessTypeInformationService
    {
        private readonly HttpClient _httpClient;
        private readonly IBusinessRepo _repo;

        public BusinessTypeInformationService(HttpClient httpClient, IBusinessRepo repo)
        {
            _httpClient = httpClient;
            _repo = repo;
        }

        public async Task<List<TypeInformationDto>> FetchTypeInformationsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<TypeInformationDto>>("api/TypeInformations");
            return response ?? new List<TypeInformationDto>();
        }

        public async Task<List<TypeInformationDto>> FetchAndCombineTypesAsync()
        {
            try
            {
                var response1 = await _httpClient.GetFromJsonAsync<List<TypeInformationDto>>("api/TypeInformations/1");
                var response2 = await _httpClient.GetFromJsonAsync<List<TypeInformationDto>>("api/TypeInformations/2");

                return (response1 ?? new List<TypeInformationDto>())
                    .Concat(response2 ?? new List<TypeInformationDto>())
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to fetch combined type information.", ex);
            }
        }

        public async Task<List<TypeInformationDto>> FetchStoreAndReturnTypeInformationsAsync()
        {
            var combinedResponse = await FetchAndCombineTypesAsync();

            if (!combinedResponse.Any())
                return new List<TypeInformationDto>();

            var entities = combinedResponse.Select(item => new BusinessData
            {
                Name = item.Name,
                Value = item.Value
            }).ToList();

            await _repo.AddRangeAsync(entities);

            return entities.Select(e => new TypeInformationDto
            {
                Name = e.Name,
                Value = e.Value
            }).ToList();
        }
    }
}

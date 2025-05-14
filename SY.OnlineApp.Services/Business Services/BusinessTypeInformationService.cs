using Microsoft.Extensions.Logging;
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
        private readonly ILogger<BusinessTypeInformationService> _logger;

        public BusinessTypeInformationService(HttpClient httpClient, IBusinessRepo repo, 
            ILogger<BusinessTypeInformationService> logger)
        {
            _httpClient = httpClient;
            _repo = repo;
            _logger = logger;
        }

        public async Task<List<TypeInformationDto>> FetchTypeInformationsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<TypeInformationDto>>("api/TypeInformations");
                return response ?? new List<TypeInformationDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching type information from the API.");
                throw new ApplicationException("Failed to fetch type information.", ex);
            }
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
                _logger.LogError(ex, "An error occurred while fetching and combining type information.");
                throw new ApplicationException("Failed to fetch combined type information.", ex);
            }
        }

       public async Task<List<TypeInformationDto>> FetchStoreAndReturnTypeInformationsAsync()
        {
            try
            {
                var combinedResponse = await FetchAndCombineTypesAsync();

                if (!combinedResponse.Any())
                {
                    return new List<TypeInformationDto>();
                }

                var entities = combinedResponse.Select(item => new BusinessData
                {
                    Name = item.Name,
                    Value = item.Value
                }).ToList();

                await _repo.AddRangeAsync(entities);
                
                var result = entities.Select(e => new TypeInformationDto
                {
                    Name = e.Name,
                    Value = e.Value
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching, storing, and returning type information.");
                throw new ApplicationException("Failed to fetch and store type information.", ex);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Models.Models;
using SY.OnlineApp.Repos.Repositories.Interfaces;

namespace SY.OnlineApp.Services.Business_Services
{
    public class LiabilityService : ILiabilityService
    {
        private readonly HttpClient _httpClient;
        private readonly ILiabilityRepo _repo;
        private readonly ILogger<LiabilityService> _logger;

        public LiabilityService(HttpClient httpClient, ILiabilityRepo repo, ILogger<LiabilityService> logger)
        {
            _httpClient = httpClient;
            _repo = repo;
            _logger = logger;
        }

        public async Task<List<Liability>> GetSavedLiabilitiesAsync()
        {
            try
            {
                return await _repo.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving saved liabilities.");
                throw new ApplicationException("Failed to retrieve saved liabilities.", ex);
            }
        }

        public async Task<List<TypeInformationDto>> FetchAndStoreLiabilitiesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<TypeInformationDto>>("api/TypeInformations/type/3");

                if (response == null || !response.Any())
                {
                    return new List<TypeInformationDto>();
                }

                var entities = response.Select(item => new Liability
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching and storing liabilities.");
                throw new ApplicationException("Failed to fetch and store liabilities.", ex);
            }
        }

        public async Task SaveLiabilitiesAsync(List<Liability> entries)
        {
            if (entries == null || !entries.Any())
            {
                throw new ArgumentException("No data to save.");
            }

            try
            {
                foreach (var entry in entries)
                {
                    var existing = await _repo.FindByNameAsync(entry.Name);
                    if (existing != null)
                    {
                        existing.Value = entry.Value;
                    }
                    else
                    {
                        await _repo.AddAsync(entry);
                    }
                }

                await _repo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving liabilities.");
                throw new ApplicationException("Failed to save liabilities.", ex);
            }
        }
    }
}

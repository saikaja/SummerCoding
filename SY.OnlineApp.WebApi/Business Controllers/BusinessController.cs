using Microsoft.AspNetCore.Mvc;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Services.BusinessServices;
using System.Net.Http.Json;
using SY.OnlineApp.Models.Models;
using SY.OnlineApp.Repos.Repositories.Interfaces;

namespace SY.OnlineApp.BusinessAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IBusinessRepo _repo;

        public BusinessController(IHttpClientFactory httpClientFactory, IBusinessRepo repo)
        {
            _httpClientFactory = httpClientFactory;
            _repo = repo;
        }

        [HttpGet("get-saved")]
        public async Task<IActionResult> GetSavedBusinessData()
        {
            var savedEntries = await _repo.GetAllAsync();
            return Ok(savedEntries);
        }

        [HttpGet("fetch-and-save")]
        public async Task<IActionResult> FetchAndSaveFromInteractive()
        {
            var client = _httpClientFactory.CreateClient();

            // Fetch Type 1 data
            var response1 = await client.GetFromJsonAsync<List<TypeInformationDto>>("https://localhost:7127/api/TypeInformations/1");

            // Fetch Type 2 data
            var response2 = await client.GetFromJsonAsync<List<TypeInformationDto>>("https://localhost:7127/api/TypeInformations/2");

            // Combine both responses
            var combinedResponse = (response1 ?? new List<TypeInformationDto>())
                .Concat(response2 ?? new List<TypeInformationDto>())
                .ToList();

            if (!combinedResponse.Any())
                return BadRequest("No data received");

            var entities = combinedResponse.Select(item => new BusinessData
            {
                Name = item.Name,
                Value = item.Value
            }).ToList();

            await _repo.AddRangeAsync(entities);

            var dtoList = entities.Select(e => new TypeInformationDto
            {
                Name = e.Name,
                Value = e.Value
            });

            return Ok(dtoList);
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveBusinessData([FromBody] List<BusinessData> entries)
        {
            if (entries == null || !entries.Any())
                return BadRequest("No data to save");

            try
            {
                foreach (var entry in entries)
                {
                    var existing = await _repo.FindByNameAsync(entry.Name);

                    if (existing != null)
                    {
                        existing.Value = entry.Value; // Update existing
                    }
                    else
                    {
                        await _repo.AddAsync(entry); // Add new
                    }
                }

                await _repo.SaveChangesAsync();

                return Ok(new { message = "Data saved successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving data: {ex.Message}");
            }
        }


    }
}

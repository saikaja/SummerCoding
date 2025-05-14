using Microsoft.AspNetCore.Mvc;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Services.BusinessServices;
using System.Net.Http.Json;
using SY.OnlineApp.Models.Models;
using SY.OnlineApp.Repos.Repositories.Interfaces;

namespace SY.OnlineApp.WebApi.Business_Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LiabilitiesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILiabilityRepo _repo;

        public LiabilitiesController(IHttpClientFactory httpClientFactory, ILiabilityRepo repo)
        {
            _httpClientFactory = httpClientFactory;
            _repo = repo;
        }

        [HttpGet("get-saved")]
        public async Task<IActionResult> GetSavedLiabilities()
        {
            var savedEntries = await _repo.GetAllAsync();
            return Ok(savedEntries);
        }

        [HttpGet("fetch-and-save")]
        public async Task<IActionResult> FetchAndSaveFromInteractive()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<List<TypeInformationDto>>("https://localhost:7127/api/TypeInformations/3");

            if (response == null || !response.Any())
                return BadRequest("No data received");

            var entities = response.Select(item => new Liability
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
        public async Task<IActionResult> SaveLiabilities([FromBody] List<Liability> entries)
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

                return Ok(new { message = "Liabilities saved successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving data: {ex.Message}");
            }
        }
    }
}

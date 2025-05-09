using Microsoft.AspNetCore.Mvc;
using NIA.OnlineApp.Data.Entities;
using NIA.OnlineApp.BusinessAPI.Services;
using System.Net.Http.Json;
using NIA.OnlineApp.BusinessAPI.Models;
using NIA.OnlineApp.Data.Repositories;

namespace NIA.OnlineApp.BusinessAPI.Controllers
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
            var response = await client.GetFromJsonAsync<List<TypeInformationDto>>("https://localhost:7200/api/TypeInformations");

            if (response == null || !response.Any())
                return BadRequest("No data received");

            var entities = new List<BusinessData>();

            foreach (var item in response)
            {
                entities.Add(new BusinessData
                {
                    Name = item.Name,
                    Value = item.Value
                });
            }

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
                await _repo.ClearAsync(); 
                await _repo.AddRangeAsync(entries);
                return Ok(new { message = "Data saved successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving data: {ex.Message}");
            }
        }

    }
}

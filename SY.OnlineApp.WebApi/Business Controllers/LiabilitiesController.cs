using Microsoft.AspNetCore.Mvc;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Services.BusinessServices;
using System.Net.Http.Json;
using SY.OnlineApp.Models.Models;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using SY.OnlineApp.Services.Business_Services;

namespace SY.OnlineApp.WebApi.Business_Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LiabilitiesController : ControllerBase
    {
        private readonly ILiabilityService _service;

        public LiabilitiesController(ILiabilityService service)
        {
            _service = service;
        }

        [HttpGet("get-saved")]
        public async Task<IActionResult> GetSavedLiabilities()
        {
            var savedEntries = await _service.GetSavedLiabilitiesAsync();
            return Ok(savedEntries);
        }

        [HttpGet("fetch-and-save")]
        public async Task<IActionResult> FetchAndSaveFromInteractive()
        {
            var result = await _service.FetchAndStoreLiabilitiesAsync();

            if (!result.Any())
                return BadRequest("No data received");

            return Ok(result);
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveLiabilities([FromBody] List<Liability> entries)
        {
            try
            {
                await _service.SaveLiabilitiesAsync(entries);
                return Ok(new { message = "Liabilities saved successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving data: {ex.Message}");
            }
        }
    }
}
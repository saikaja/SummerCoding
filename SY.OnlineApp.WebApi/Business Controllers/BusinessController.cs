using Microsoft.AspNetCore.Mvc;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Models.Models;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using SY.OnlineApp.Services.BusinessServices;

namespace SY.OnlineApp.BusinessAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IBusinessTypeInformationService _typeInfoService;
        private readonly IBusinessRepo _repo;

        public BusinessController(
            IHttpClientFactory httpClientFactory,
            IBusinessTypeInformationService typeInfoService,
            IBusinessRepo repo)
        {
            _httpClientFactory = httpClientFactory;
            _typeInfoService = typeInfoService;
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
            try
            {
                var result = await _typeInfoService.FetchStoreAndReturnTypeInformationsAsync();

                if (result == null || !result.Any())
                    return StatusCode(422);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SY.OnlineApp.Data.Entities;
using System.Net.Http.Json;
using SY.OnlineApp.Models.Models;
using SY.OnlineApp.Repos.Repositories;
using SY.OnlineApp.Services.Integrated_Type_Services;

namespace SY.OnlineApp.WebApi.Integrated_Type_Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IntegratedTypeController : ControllerBase
    {
        private readonly IIntegratedTypeService _service;

        public IntegratedTypeController(IIntegratedTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var types = await _service.GetAllAsync();
            return Ok(types);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IntegratedType type)
        {
            if (string.IsNullOrWhiteSpace(type.Type))
                return BadRequest("Type is required.");

            var success = await _service.CreateTypeAsync(type);
            if (!success)
                return Conflict("Type already exists.");

            return Ok(type);
        }
    }
}

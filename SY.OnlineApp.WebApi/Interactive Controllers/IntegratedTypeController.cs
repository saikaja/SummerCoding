using Microsoft.AspNetCore.Mvc;
using SY.OnlineApp.Data.Entities;
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
            var success = await _service.CreateTypeAsync(type);
            return success ? Ok(type) : Conflict("Type already exists.");
        }
    }
}


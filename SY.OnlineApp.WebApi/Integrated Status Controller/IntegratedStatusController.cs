using Microsoft.AspNetCore.Mvc;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Services.Integrated_Status_Services;

namespace SY.OnlineApp.WebApi.Integrated_Status_Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class IntegratedStatusController : ControllerBase
    {
        private readonly IIntegratedStatusService _statusService;

        public IntegratedStatusController(IIntegratedStatusService statusService)
        {
            _statusService = statusService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateStatus([FromBody] IntegratedStatus status)
        {
            if (status == null || status.IntegratedId <= 0)
                return BadRequest("Invalid status data.");

            await _statusService.AddStatusAsync(status);
            return Ok(status);
        }

        // GET: api/IntegratedStatus/{integratedTypeId}
        [HttpGet("{integratedTypeId}")]
        public async Task<IActionResult> GetStatus(int integratedTypeId)
        {
            var status = await _statusService.GetStatusByTypeAsync(integratedTypeId);
            if (status == null)
                return NotFound();

            return Ok(status);
        }

        // PUT: api/IntegratedStatus/{integratedTypeId}/update-status?isIntegrated=true
        [HttpPut("{integratedTypeId}/update-status")]
        public async Task<IActionResult> UpdateStatus(int integratedTypeId, [FromQuery] bool isIntegrated)
        {
            var result = await _statusService.UpdateIntegrationStatusAsync(integratedTypeId, isIntegrated);
            if (!result)
                return NotFound("Status update failed or record not found.");

            return Ok("Status updated successfully.");
        }
    }
}

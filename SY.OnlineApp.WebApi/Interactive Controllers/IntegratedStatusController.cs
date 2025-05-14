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
            await _statusService.CreateStatusAsync(status);
            return Ok();
        }

        [HttpGet("{integratedTypeId}")]
        public async Task<IActionResult> GetStatus(int integratedTypeId)
        {
            var status = await _statusService.GetStatusByTypeAsync(integratedTypeId);
            return Ok(status);
        }

        [HttpPut("{integratedTypeId}/update-status")]
        public async Task<IActionResult> UpdateStatus(int integratedTypeId, [FromQuery] bool isIntegrated)
        {
            await _statusService.UpdateStatusAsync(integratedTypeId, isIntegrated);
            return Ok();
        }
    }
}

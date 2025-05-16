using Microsoft.AspNetCore.Mvc;
using SY.OnlineApp.Services.Interfaces;
using System.Threading.Tasks;

namespace SY.OnlineApp.AuthenticationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LastLoginController : ControllerBase
    {
        private readonly ILastLoginService _service;

        public LastLoginController(ILastLoginService service)
        {
            _service = service;
        }

        [HttpPost("record/{registrationId}")]
        public async Task<IActionResult> Record(int registrationId)
        {
            await _service.RecordLoginAsync(registrationId);
            return Ok("Login recorded.");
        }

        [HttpGet("{registrationId}")]
        public async Task<IActionResult> Get(int registrationId)
        {
            var lastLogin = await _service.GetLastLoginAsync(registrationId);
            if (lastLogin == null)
                return NotFound("No login record found.");

            return Ok(lastLogin);
        }
    }
}

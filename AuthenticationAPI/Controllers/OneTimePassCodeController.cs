using Microsoft.AspNetCore.Mvc;
using SY.OnlineApp.Services.Business_Services.Interfaces;
using SY.OnlineApp.Services.Interfaces;
using System.Threading.Tasks;

namespace SY.OnlineApp.AuthenticationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OneTimePassCodeController : ControllerBase
    {
        private readonly IOneTimePassCodeService _service;

        public OneTimePassCodeController(IOneTimePassCodeService service)
        {
            _service = service;
        }

        [HttpPost("generate/{registrationId}")]
        public async Task<IActionResult> Generate(int registrationId)
        {
            var code = await _service.GenerateAndSaveOtpAsync(registrationId);
            return Ok(new { OneTimeCode = code });
        }

        [HttpPost("validate/{registrationId}")]
        public async Task<IActionResult> Validate(int registrationId, [FromQuery] string code)
        {
            var isValid = await _service.ValidateOtpAsync(registrationId, code);
            return isValid ? Ok("OTP is valid.") : BadRequest("Invalid or expired OTP.");
        }
    }
}

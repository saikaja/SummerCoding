using Microsoft.AspNetCore.Mvc;
using SY.OnlineApp.Models.Dtos;
using SY.OnlineApp.Services.Business_Services.Interfaces;
using SY.OnlineApp.Services.Interfaces;
using SY.OnlineApp.Services.Services;
using System.Threading.Tasks;

namespace SY.OnlineApp.AuthenticationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OneTimePassCodeController : ControllerBase
    {
        private readonly IOneTimePassCodeService _service;
        private readonly IRegisterService _registerService;

        public OneTimePassCodeController(IOneTimePassCodeService service, IRegisterService registerService)
        {
            _service = service;
            _registerService = registerService;
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

        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] RegisterDto dto)
        {
            await _registerService.RegisterUserAsync(dto);
            return Ok("OTP sent to your email.");
        }

    }
}

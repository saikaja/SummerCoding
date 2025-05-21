using Microsoft.AspNetCore.Mvc;
using SY.OnlineApp.Models.Dtos;
using SY.OnlineApp.Models.Models;
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
            return isValid ? Ok("OTP is valid.") : BadRequest(new { message = "Invalid or expired OTP." });
        }

        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] RegisterDto dto)
        {
            try
            {
                var result = await _registerService.RegisterUserAsync(dto);
                return Ok(new { message = result });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message }); // returns "Username already exists."
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Unexpected error occurred." });
            }
        }


        [HttpPost("create-password")]
        public async Task<IActionResult> CreatePassword([FromBody] CreatePasswordDto dto)
        {
            try
            {
                await _registerService.SetPasswordAsync(dto);
                return Ok(new { message = "Password created successfully. Account activated." });
            }
            catch(ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

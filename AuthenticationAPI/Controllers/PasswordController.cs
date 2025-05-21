using Microsoft.AspNetCore.Mvc;
using SY.OnlineApp.Models.Models;
using SY.OnlineApp.Services.Business_Services.Interfaces;

namespace SY.OnlineApp.AuthenticationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordService _service;

        public PasswordController(IPasswordService service)
        {
            _service = service;
        }

        [HttpPost("forgot")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            await _service.SendPasswordResetOtpAsync(dto);
            return Ok("OTP sent to your email.");
        }

        [HttpPost("reset")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            try
            {
                await _service.ResetPasswordAsync(dto);
                return Ok("Password reset successful.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}

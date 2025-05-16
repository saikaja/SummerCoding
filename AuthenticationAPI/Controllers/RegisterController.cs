using Microsoft.AspNetCore.Mvc;
using SY.OnlineApp.Services.Services;

namespace SY.OnlineApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly RegisterService _service;

        public RegisterController(RegisterService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.RegisterUserAsync(dto);
                return Ok("User registered successfully.");
            }
            catch (ArgumentException ex)
            {
                return Conflict(new { message = ex.Message }); // 409 Conflict
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
namespace AuthenticationAPI.Controllers
{
    public class RegisterController
    {
    }
}

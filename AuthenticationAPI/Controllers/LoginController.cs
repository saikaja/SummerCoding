using Microsoft.AspNetCore.Mvc;
using SY.OnlineApp.Models.Models;
using SY.OnlineApp.Services.Business_Services.Interfaces;

namespace SY.OnlineApp.AuthenticationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var token = await _loginService.AuthenticateAsync(dto);

            if (string.IsNullOrEmpty(token))
                return Unauthorized(new { message = "Invalid username or password." });

            return Ok(new { Token = token });
        }
    }

}

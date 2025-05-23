using Microsoft.AspNetCore.Mvc;
using SY.OnlineApp.Models.Models;
using SY.OnlineApp.Services.Business_Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginController(ILoginService loginService, IJwtTokenService jwtTokenService)
    {
        _loginService = loginService;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        var user = await _loginService.AuthenticateAsync(dto);
        if (user == null)
            return Unauthorized(new { message = "Invalid username or password." });

        return Ok(user);
    }

    [HttpPost("refresh")]
    public IActionResult Refresh([FromBody] RefreshTokenRequest request)
    {
        var principal = _jwtTokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        if (principal == null)
            return BadRequest("Invalid token.");

        var username = principal.Identity?.Name;
        var newTokens = _jwtTokenService.GenerateTokens(username!);

        return Ok(new
        {
            accessToken = newTokens.AccessToken,
            refreshToken = newTokens.RefreshToken
        });
    }
}

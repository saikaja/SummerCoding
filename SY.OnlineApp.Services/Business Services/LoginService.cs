using Microsoft.Extensions.Logging;
using SY.OnlineApp.Models.Models;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using SY.OnlineApp.Services.Business_Services.Interfaces;
using SY.OnlineApp.Services.Interfaces;

public class LoginService : ILoginService
{
    private readonly IRegisterRepo _registerRepo;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ILogger<LoginService> _logger;
    private readonly ILastLoginService _lastLoginService;

    public LoginService(IRegisterRepo registerRepo, IJwtTokenService jwtTokenService, ILogger<LoginService> logger, ILastLoginService lastLoginService)
    {
        _registerRepo = registerRepo;
        _jwtTokenService = jwtTokenService;
        _lastLoginService = lastLoginService;
        _logger = logger;
    }

    public async Task<LoginResponseDto?> AuthenticateAsync(LoginRequestDto dto)
    {
        try
        {
            var user = await _registerRepo.GetByUserNameAsync(dto.UserName);

            if (user == null || string.IsNullOrEmpty(user.PasswordHash))
                return null;

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!isValidPassword)
                return null;

            await _lastLoginService.RecordLoginAsync(user.Id);

            var tokens = _jwtTokenService.GenerateTokens(user.UserName);

            return new LoginResponseDto
            {
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RegistrationId = user.Id,
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login failed for user {UserName}", dto.UserName);
            return null;
        }
    }
}

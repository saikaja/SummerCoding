using SY.OnlineApp.Models.Dtos;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using SY.OnlineApp.Services.Interfaces;
using BCrypt.Net;
using Microsoft.Extensions.Logging;
using SY.OnlineApp.Models.Models;
using SY.OnlineApp.Services.Business_Services.Interfaces;

namespace SY.OnlineApp.Services.Business_Services
{
    public class LoginService : ILoginService
    {
        private readonly IRegisterRepo _registerRepo;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILogger<LoginService> _logger;

        public LoginService(IRegisterRepo registerRepo, IJwtTokenService jwtTokenService, ILogger<LoginService> logger)
        {
            _registerRepo = registerRepo;
            _jwtTokenService = jwtTokenService;
            _logger = logger;
        }

        public async Task<string> AuthenticateAsync(LoginRequestDto dto)
        {
            try
            {
                var user = await _registerRepo.GetByUserNameAsync(dto.UserName);

                if (user == null || string.IsNullOrEmpty(user.PasswordHash))
                    return null;

                bool isValidPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

                if (!isValidPassword)
                    return null;

                return _jwtTokenService.GenerateToken(user.UserName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login failed for user {UserName}", dto.UserName);
                return null;
            }
        }
    }
}

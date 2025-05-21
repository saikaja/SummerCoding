using System;
using System.Threading.Tasks;
using SY.OnlineApp.Models.Dtos;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using SY.OnlineApp.Services.Interfaces;
using SY.OnlineApp.Services.Business_Services.Interfaces;
using Microsoft.Extensions.Logging;
using BCrypt.Net;
using SY.OnlineApp.Models.Models;

namespace SY.OnlineApp.Services.Business_Services
{
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

                return new LoginResponseDto
                {
                    Token = _jwtTokenService.GenerateToken(user.UserName),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    RegistrationId = user.Id
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login failed for user {UserName}", dto.UserName);
                return null;
            }
        }
    }
}

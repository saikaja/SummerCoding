using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using SY.OnlineApp.Models.Models;
using SY.OnlineApp.Services.Business_Services.Interfaces;

namespace SY.OnlineApp.Services.Business_Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettings _settings;
        private readonly ILogger<JwtTokenService> _logger;

        public JwtTokenService(IOptions<JwtSettings> settings, ILogger<JwtTokenService> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public string GenerateToken(string userName)
        {
            try
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _settings.Issuer,
                    audience: _settings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_settings.ExpiresInMinutes),
                    signingCredentials: creds);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return tokenString;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate JWT token for user {UserName}", userName);
                return string.Empty;
            }
        }
    }
}

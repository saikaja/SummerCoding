using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SY.OnlineApp.Models.Models;
using SY.OnlineApp.Services.Business_Services.Interfaces;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _settings;

    public JwtTokenService(IOptions<JwtSettings> settings)
    {
        _settings = settings.Value;
    }

    public TokenResponseDto GenerateTokens(string userName)
    {
        var accessToken = GenerateJwtToken(userName, TimeSpan.FromMinutes(_settings.ExpiresInMinutes));
        var refreshToken = GenerateJwtToken(userName, TimeSpan.FromDays(7)); // Refresh token valid 7 days

        return new TokenResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    private string GenerateJwtToken(string userName, TimeSpan expiry)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, userName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(expiry),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key)),
            ValidateLifetime = false,
            ValidIssuer = _settings.Issuer,
            ValidAudience = _settings.Audience,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var _);
        return principal;
    }
}

using SY.OnlineApp.Models.Models;
using System.Security.Claims;

namespace SY.OnlineApp.Services.Business_Services.Interfaces
{
    public interface IJwtTokenService
    {
        TokenResponseDto GenerateTokens(string userName);
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}

using System.Security.Cryptography;
using System.Text;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using SY.OnlineApp.Services.Business_Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace SY.OnlineApp.Services.Business_Services
{
    public class OneTimePassCodeService : IOneTimePassCodeService
    {
        private readonly IOneTimePassCodeRepo _repo;
        private readonly ILogger<OneTimePassCodeService> _logger;

        public OneTimePassCodeService(IOneTimePassCodeRepo repo, ILogger<OneTimePassCodeService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<string> GenerateAndSaveOtpAsync(int registrationId)
        {
            try
            {
                var otp = GenerateSecureOtp(8);

                var code = new OneTimePassCode
                {
                    RegistrationId = registrationId,
                    OneCode = otp,
                    ExpirationTime = DateTime.UtcNow.AddMinutes(5),
                    Used = false
                };

                await _repo.AddAsync(code);

                _logger.LogInformation("OTP generated and saved for RegistrationId: {RegistrationId}", registrationId);
                return otp;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating OTP for RegistrationId: {RegistrationId}", registrationId);
                throw;
            }
        }

        public async Task<bool> ValidateOtpAsync(int registrationId, string code)
        {
            try
            {
                var otp = await _repo.GetValidCodeAsync(registrationId, code);
                var result = otp != null;

                if (result)
                    _logger.LogInformation("OTP validation succeeded for RegistrationId: {RegistrationId}", registrationId);
                else
                    _logger.LogWarning("OTP validation failed for RegistrationId: {RegistrationId}", registrationId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating OTP for RegistrationId: {RegistrationId}", registrationId);
                return false;
            }
        }

        private string GenerateSecureOtp(int length)
        {
            var bytes = new byte[length];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);

            var sb = new StringBuilder();
            foreach (var b in bytes)
                sb.Append(b % 10);

            return sb.ToString();
        }
    }
}

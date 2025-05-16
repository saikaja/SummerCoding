using System.Security.Cryptography;
using System.Text;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using SY.OnlineApp.Services.Business_Services.Interfaces;

namespace SY.OnlineApp.Services.Business_Services
{
    public class OneTimePassCodeService : IOneTimePassCodeService
    {
        private readonly IOneTimePassCodeRepo _repo;

        public OneTimePassCodeService(IOneTimePassCodeRepo repo)
        {
            _repo = repo;
        }

        public async Task<string> GenerateAndSaveOtpAsync(int registrationId)
        {
            var otp = GenerateSecureOtp(8); // 8-digit secure code

            var code = new OneTimePassCode
            {
                RegistrationId = registrationId,
                OneCode = otp,
                ExpirationTime = DateTime.UtcNow.AddMinutes(5),
                Used = false
            };

            await _repo.AddAsync(code);
            return otp;
        }

        public async Task<bool> ValidateOtpAsync(int registrationId, string code)
        {
            var otp = await _repo.GetValidCodeAsync(registrationId, code);
            return otp != null;
        }

        private string GenerateSecureOtp(int length)
        {
            var bytes = new byte[length];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);

            var sb = new StringBuilder();
            foreach (var b in bytes)
                sb.Append(b % 10); // Only digits

            return sb.ToString();
        }
    }
}

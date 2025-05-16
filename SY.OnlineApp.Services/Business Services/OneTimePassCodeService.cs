using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var random = new Random();
            var otp = random.Next(10000000, 99999999).ToString();

            var code = new OneTimePassCode
            {
                RegistrationId = registrationId,
                OneCode = otp,
                ExpirationTime = DateTime.UtcNow.AddMinutes(5)
            };

            await _repo.AddAsync(code);

            return otp;
        }

        public async Task<bool> ValidateOtpAsync(int registrationId, string code)
        {
            var otp = await _repo.GetValidCodeAsync(registrationId, code);
            return otp != null;
        }
    }
}

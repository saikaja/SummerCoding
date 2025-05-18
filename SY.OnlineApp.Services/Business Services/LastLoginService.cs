using System;
using System.Threading.Tasks;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using SY.OnlineApp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace SY.OnlineApp.Services.Business_Services
{
    public class LastLoginService : ILastLoginService
    {
        private readonly ILastLoginRepo _repo;
        private readonly ILogger<LastLoginService> _logger;

        public LastLoginService(ILastLoginRepo repo, ILogger<LastLoginService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task RecordLoginAsync(int registrationId)
        {
            try
            {
                var login = new LastLogin
                {
                    RegistrationId = registrationId,
                    LoginTimestamp = DateTime.UtcNow
                };

                await _repo.AddAsync(login);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error recording login for RegistrationId: {RegistrationId}", registrationId);
            }
        }

        public async Task<LastLogin?> GetLastLoginAsync(int registrationId)
        {
            try
            {
                var lastLogin = await _repo.GetLastLoginAsync(registrationId);
                return lastLogin;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving last login for RegistrationId: {RegistrationId}", registrationId);
                return null;
            }
        }
    }
}

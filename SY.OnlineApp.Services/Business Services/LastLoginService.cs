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
                await _repo.AddOrUpdateAsync(registrationId);
                _logger.LogInformation("✅ Last login recorded or updated for RegistrationId: {RegistrationId}", registrationId);
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
                return await _repo.GetLastLoginAsync(registrationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving last login for RegistrationId: {RegistrationId}", registrationId);
                return null;
            }
        }
    }
}

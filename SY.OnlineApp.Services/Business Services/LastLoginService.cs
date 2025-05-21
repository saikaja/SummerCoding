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
                var existingLogin = await _repo.GetLastLoginAsync(registrationId);

                if (existingLogin != null)
                {
                    existingLogin.LoginTimestamp = DateTime.UtcNow;
                    await _repo.UpdateAsync(existingLogin);
                    _logger.LogInformation("Updated existing last login for RegistrationId: {RegistrationId}", registrationId);
                }
                else
                {
                    // Use AddOrUpdateAsync instead of AddAsync as per the ILastLoginRepo interface
                    await _repo.AddOrUpdateAsync(registrationId);
                    _logger.LogInformation("Created new last login record for RegistrationId: {RegistrationId}", registrationId);
                }
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

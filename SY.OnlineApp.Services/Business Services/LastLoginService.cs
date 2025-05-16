using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using SY.OnlineApp.Services.Interfaces;

namespace SY.OnlineApp.Services.Business_Services
{
    public class LastLoginService : ILastLoginService
    {
        private readonly ILastLoginRepo _repo;

        public LastLoginService(ILastLoginRepo repo)
        {
            _repo = repo;
        }

        public async Task RecordLoginAsync(int registrationId)
        {
            var login = new LastLogin
            {
                RegistrationId = registrationId,
                LoginTimestamp = DateTime.UtcNow
            };

            await _repo.AddAsync(login);
        }

        public async Task<LastLogin?> GetLastLoginAsync(int registrationId)
        {
            return await _repo.GetLastLoginAsync(registrationId);
        }
    }
}

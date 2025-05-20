using SY.OnlineApp.Data.Entities;
using System.Threading.Tasks;

namespace SY.OnlineApp.Repos.Repositories.Interfaces
{
    public interface ILastLoginRepo
    {
        /// <summary>
        /// Inserts a new LastLogin record or updates the timestamp if one already exists.
        /// </summary>
        /// <param name="registrationId">The RegistrationId to log against.</param>
        Task AddOrUpdateAsync(int registrationId);

        /// <summary>
        /// Retrieves the last login record for a specific RegistrationId.
        /// </summary>
        /// <param name="registrationId">The RegistrationId to search for.</param>
        /// <returns>The LastLogin record if found, otherwise null.</returns>
        Task<LastLogin?> GetLastLoginAsync(int registrationId);
        Task UpdateAsync(LastLogin login);

    }
}

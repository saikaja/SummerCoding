using SY.OnlineApp.Data.Entities;
using System.Threading.Tasks;

namespace SY.OnlineApp.Repos.Repositories.Interfaces
{
    public interface ILastLoginRepo
    {
        Task AddAsync(LastLogin login);
        Task<LastLogin?> GetLastLoginAsync(int registrationId);
        Task UpdateAsync(LastLogin login);

    }
}

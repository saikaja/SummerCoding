using SY.OnlineApp.Data.Entities;
using System.Threading.Tasks;

namespace SY.OnlineApp.Repos.Repositories.Interfaces
{
    public interface IOneTimePassCodeRepo
    {
        Task AddAsync(OneTimePassCode code);
        Task<OneTimePassCode?> GetValidCodeAsync(int registrationId, string code);
    }
}

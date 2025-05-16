using System.Threading.Tasks;
using SY.OnlineApp.Data.Entities;

namespace SY.OnlineApp.Services.Interfaces
{
    public interface ILastLoginService
    {
        Task RecordLoginAsync(int registrationId);
        Task<LastLogin?> GetLastLoginAsync(int registrationId);
    }
}

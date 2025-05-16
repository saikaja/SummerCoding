using System.Threading.Tasks;
using SY.OnlineApp.Models.Dtos;

namespace SY.OnlineApp.Services.Interfaces
{
    public interface IRegisterService
    {
        Task RegisterUserAsync(RegisterDto dto);
    }
}

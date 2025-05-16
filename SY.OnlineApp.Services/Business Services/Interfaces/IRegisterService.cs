using System.Threading.Tasks;
using SY.OnlineApp.Models.Dtos;
using SY.OnlineApp.Models.Models;

namespace SY.OnlineApp.Services.Interfaces
{
    public interface IRegisterService
    {
        Task RegisterUserAsync(RegisterDto dto);
        Task SetPasswordAsync(CreatePasswordDto dto);

    }
}

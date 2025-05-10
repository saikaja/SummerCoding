using SY.OnlineApp.Data.Entities;

namespace SY.OnlineApp.Services.InteractiveServices
{
    public interface IInteractiveTypeUtilService
    {
        Task<IEnumerable<TypeUtil>> GetAllAsync();
        Task<TypeUtil?> GetByIdAsync(int id);
        Task<bool> AddAsync(TypeUtil typeUtil);
        Task<bool> UpdateAsync(TypeUtil typeUtil);
        Task<bool> DeleteAsync(TypeUtil typeUtil);
    }
}

using NIA.OnlineApp.Data.Entities;

namespace NIA.OnlineApp.InteractiveAPI.Services
{
    public interface ITypeUtilService
    {
        Task<IEnumerable<TypeUtil>> GetAllAsync();
        Task<TypeUtil?> GetByIdAsync(int id);
        Task<bool> AddAsync(TypeUtil typeUtil);
        Task<bool> UpdateAsync(TypeUtil typeUtil);
        Task<bool> DeleteAsync(TypeUtil typeUtil);
    }
}

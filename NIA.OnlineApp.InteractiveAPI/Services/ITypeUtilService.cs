using NIA.OnlineApp.Data.Entities;

namespace NIA.OnlineApp.InteractiveAPI.Services
{
    public interface ITypeUtilService
    {
        Task<IEnumerable<TypeUtil>> GetAllAsync();
        Task<TypeUtil?> GetByIdAsync(int id);
        Task<bool> AddAsync(int id, TypeUtil typeUtil);
        Task<bool> UpdateAsync(int id, TypeUtil typeUtil);
        Task<bool> DeleteAsync(int id, TypeUtil typeUtil);
    }
}

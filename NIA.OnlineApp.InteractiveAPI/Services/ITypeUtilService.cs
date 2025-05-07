using NIA.OnlineApp.Data.Entities;

namespace NIA.OnlineApp.InteractiveAPI.Services
{
    public interface ITypeUtilService
    {
        Task<IEnumerable<TypeUtil>> GetAllEventsAsync();
        Task<TypeUtil?> GetByIdAsync(int id);
        Task<bool> InsertEventAsync(TypeUtil typeUtil);
        Task<bool> UpdateEventAsync(TypeUtil typeUtil);
        Task<bool> DeleteEventAsync(int id);
    }
}

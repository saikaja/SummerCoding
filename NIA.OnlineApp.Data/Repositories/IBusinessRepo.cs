using NIA.OnlineApp.Data.Entities;

namespace NIA.OnlineApp.Data.Repositories
{
    public interface IBusinessRepo
    {
        Task AddRangeAsync(IEnumerable<BusinessData> entries);
        Task<List<BusinessData>> GetAllAsync();
        Task ClearAsync();
    }
}

using SY.OnlineApp.Data.Entities;

namespace SY.OnlineApp.Repos.Repositories.Interfaces
{
    public interface IBusinessRepo
    {
        Task AddRangeAsync(IEnumerable<BusinessData> entries);
        Task ClearAsync();
        Task<List<BusinessData>> GetAllAsync();
        Task<BusinessData> FindByNameAsync(string name);
        Task AddAsync(BusinessData entry);
        Task SaveChangesAsync();
    }

}

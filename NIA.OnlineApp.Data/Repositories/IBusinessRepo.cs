using SY.OnlineApp.Data.Entities;

namespace SY.OnlineApp.Data.Repositories
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

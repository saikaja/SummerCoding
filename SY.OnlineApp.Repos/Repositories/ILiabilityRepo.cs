using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SY.OnlineApp.Data.Entities;

namespace SY.OnlineApp.Repos.Repositories
{
    public interface ILiabilityRepo
    {
        Task AddRangeAsync(IEnumerable<Liability> entries);
        Task ClearAsync();
        Task<List<Liability>> GetAllAsync();
        Task<Liability> FindByNameAsync(string name);
        Task AddAsync(Liability entry);
        Task SaveChangesAsync();
    }
}

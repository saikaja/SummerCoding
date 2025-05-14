using System.Collections.Generic;
using System.Threading.Tasks;
using SY.OnlineApp.Data.Entities;

namespace SY.OnlineApp.Repos.Repositories.Interfaces
{
    public interface ILogEntryRepo
    {
        Task AddAsync(LogEntry log);
        Task<IEnumerable<LogEntry>> GetAllAsync();
    }
}

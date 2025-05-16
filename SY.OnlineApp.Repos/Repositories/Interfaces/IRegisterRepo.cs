using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SY.OnlineApp.Data.Entities;

namespace SY.OnlineApp.Repos.Repositories.Interfaces
{
    public interface IRegisterRepo
    {
        Task AddAsync(Register register);
        Task SaveAsync();
        Task<bool> UserNameExistsAsync(string username);
    }
}

﻿using SY.OnlineApp.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SY.OnlineApp.Repos.Repositories.Interfaces
{
    public interface ITypeUtilRepo
    {
        Task<IEnumerable<TypeUtil>> GetAllAsync();
        Task<TypeUtil?> GetByIdAsync(int id);
        Task AddAsync(TypeUtil typeUtil);
        Task UpdateAsync(TypeUtil typeUtil);
        Task DeleteAsync(TypeUtil typeUtil);
    }

}

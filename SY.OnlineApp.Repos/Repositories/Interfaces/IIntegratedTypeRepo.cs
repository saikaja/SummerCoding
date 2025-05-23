﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SY.OnlineApp.Data.Entities;

namespace SY.OnlineApp.Repos.Repositories.Interfaces
{
    public interface IIntegratedTypeRepo
    {
        Task<IEnumerable<IntegratedType>> GetAllAsync();
        Task<IntegratedType?> GetByIdAsync(int id);
        Task AddAsync(IntegratedType type);
        Task<bool> ExistsAsync(string type);
    }

}

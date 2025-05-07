using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NIA.OnlineApp.Data.Entities;

namespace NIA.OnlineApp.Data
{
    public class IntegratedDbContext : DbContext
    {
        public IntegratedDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TypeUtil> type { get; set; }
        public DbSet<TypeInformation> typeInformation { get; set; }
    }
}

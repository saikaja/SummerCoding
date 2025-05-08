using Microsoft.EntityFrameworkCore;
using NIA.OnlineApp.Data.Configurations;
using NIA.OnlineApp.Data.Entities;

namespace NIA.OnlineApp.Data
{
    public class BusinessDbContext : DbContext
    {
        public BusinessDbContext(DbContextOptions<BusinessDbContext> options) : base(options) { }

        public DbSet<BusinessData> BusinessEntries { get; set; }
    }

}

using Microsoft.EntityFrameworkCore;
using SY.OnlineApp.Data.Configurations;
using SY.OnlineApp.Data.Entities;

namespace SY.OnlineApp.Data
{
    public class BusinessDbContext : DbContext
    {
        public BusinessDbContext(DbContextOptions<BusinessDbContext> options) : base(options) { }

        public DbSet<BusinessData> BusinessEntries { get; set; }
    }

}

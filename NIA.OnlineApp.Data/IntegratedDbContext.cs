using Microsoft.EntityFrameworkCore;
using SY.OnlineApp.Data.Configurations;
using SY.OnlineApp.Data.Entities;

namespace SY.OnlineApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TypeUtil> Types { get; set; }
        public DbSet<TypeInformation> TypeInformations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TypeConfig());
        }
    }
}

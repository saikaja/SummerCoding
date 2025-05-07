using Microsoft.EntityFrameworkCore;
using NIA.OnlineApp.Data.Configurations;
using NIA.OnlineApp.Data.Entities;

namespace NIA.OnlineApp.Data
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

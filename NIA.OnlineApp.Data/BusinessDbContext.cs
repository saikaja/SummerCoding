using Microsoft.EntityFrameworkCore;
using SY.OnlineApp.Data.Entities;

namespace SY.OnlineApp.Data
{
    public class BusinessDbContext : DbContext
    {
        public BusinessDbContext(DbContextOptions<BusinessDbContext> options) : base(options) { }

        public DbSet<BusinessData> BusinessEntries { get; set; }
        public DbSet<IntegratedType> IntegratedTypes { get; set; }
        public DbSet<IntegratedStatus> IntegratedStatuses { get; set; }
        public DbSet<Liability> Liabilities { get; set; }
        public DbSet<LogEntry> Logs { get; set; }
        public DbSet<Register> Registrations { get; set; }
        public DbSet<OneTimePassCode> OneTimePassCodes { get; set; }
        public DbSet<LastLogin> LastLogins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IntegratedType>()
                .HasMany(t => t.IntegratedStatuses)
                .WithOne(s => s.IntegratedType)
                .HasForeignKey(s => s.IntegratedId);

            modelBuilder.Entity<Register>()
                .HasIndex(r => r.UserName)
                .IsUnique();

            modelBuilder.Entity<Register>()
                .Property(r => r.Id)
                .ValueGeneratedOnAdd(); // ✅ Ensures Register.Id is treated as an identity column

            base.OnModelCreating(modelBuilder);
        }
    }
}

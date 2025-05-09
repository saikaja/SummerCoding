using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SY.OnlineApp.Data.Entities;

namespace SY.OnlineApp.Data.Configurations
{
    public class TypeConfig : IEntityTypeConfiguration<TypeUtil>
    {
        public void Configure(EntityTypeBuilder<TypeUtil> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Type)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasMany(t => t.TypeInformation)
                   .WithOne(ti => ti.TypeUtil)
                   .HasForeignKey(ti => ti.Type_Id);
        }
    }
}


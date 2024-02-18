using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseMS.Infrastructure.Context.Entities;

namespace WarehouseMS.Infrastructure.Context.Configs;

public class StatusViewConfig : IEntityTypeConfiguration<StatusView>
{
    public void Configure(EntityTypeBuilder<StatusView> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.Color)
            .HasMaxLength(30);

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("NOW()");
    }
}
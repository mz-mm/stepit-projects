using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseMS.Infrastructure.Context.Entities;

namespace WarehouseMS.Infrastructure.Context.Configs;

public class WarehouseConfig : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.Property(w => w.UserId)
            .IsRequired();

        builder.Property(w => w.ProductId)
            .IsRequired();

        builder.Property(w => w.TrackingId)
            .IsRequired();

        builder.Property(w => w.OrderStatus)
            .IsRequired();

        builder.Property(w => w.CreatedDate)
            .HasDefaultValue(DateTime.UtcNow);

        builder.HasOne(w => w.User)
            .WithMany(u => u.Warehouses)
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(w => w.Product)
            .WithMany(p => p.Warehouses)
            .HasForeignKey(w => w.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

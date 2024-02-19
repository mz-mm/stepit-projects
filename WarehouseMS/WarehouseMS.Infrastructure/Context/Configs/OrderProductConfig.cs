using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseMS.Infrastructure.Context.Entities;

namespace WarehouseMS.Infrastructure.Context.Configs;

public class OrderProductConfig : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder.HasKey(cs => new { cs.OrderId, cs.ProductId });

        builder.HasOne(cs => cs.Order)
            .WithMany(s => s.OrderProducts)
            .HasForeignKey(cs => cs.OrderId);

        builder.HasOne(cs => cs.Product)
            .WithMany(c => c.OrderProducts)
            .HasForeignKey(cs => cs.ProductId);
    }
}
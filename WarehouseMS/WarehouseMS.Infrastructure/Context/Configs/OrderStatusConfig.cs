using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseMS.Infrastructure.Context.Entities;

namespace WarehouseMS.Infrastructure.Context.Configs;

public class OrderStatusConfig : IEntityTypeConfiguration<OrderStatus>   
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.Property(x => x.Status)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("NOW()");
    }
}
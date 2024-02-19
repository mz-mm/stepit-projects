using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseMS.Infrastructure.Context.Entities;

namespace WarehouseMS.Infrastructure.Context.Configs;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(250);

        builder.Property(x => x.Price)
            .HasColumnType("decimal(14,2)");

        builder.Property(x => x.StockQuantity)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("NOW()");

        builder.HasOne(x => x.StatusView)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.OrderProducts)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
namespace WarehouseMS.Infrastructure.Context.Entities;

public class StatusView : BaseEntity
{
    public string Name { get; set; }
    public string? Color { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<Product> Products { get; set; }
}
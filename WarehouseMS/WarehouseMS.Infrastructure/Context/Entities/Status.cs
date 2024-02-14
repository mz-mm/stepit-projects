namespace WarehouseMS.Infrastructure.Context.Entities;

public class Status : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
    public DateTime CretedAt { get; set; }

    public ICollection<Product> Products { get; set; }
}
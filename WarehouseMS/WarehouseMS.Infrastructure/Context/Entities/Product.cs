namespace WarehouseMS.Infrastructure.Context.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int StockQuantity { get; set; }
    public int CategoryId { get; set; }
    public DateTime CreatedAt { get; set; }

    public Category Category { get; set; }
    public ICollection<Warehouse> Warehouses { get; set; }
}

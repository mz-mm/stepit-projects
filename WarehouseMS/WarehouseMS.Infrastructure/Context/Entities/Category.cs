namespace WarehouseMS.Infrastructure.Context.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public string Icon { get; set; }
    public string Color { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<Product> Products { get; set; }
}

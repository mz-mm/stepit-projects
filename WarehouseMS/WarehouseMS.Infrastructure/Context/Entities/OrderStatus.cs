namespace WarehouseMS.Infrastructure.Context.Entities;

public class OrderStatus : BaseEntity
{
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<Order> Orders { get; set; }
}
namespace WarehouseMS.Infrastructure.Context.Entities;

public class Order : BaseEntity
{
    public int UserId { get; set; }
    public int TrackingId { get; set; }
    public int OrderStatusId { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; }

    public OrderStatus Status { get; set; }
    public User User { get; set; }
    public ICollection<OrderProduct> OrderProducts { get; set; }
}

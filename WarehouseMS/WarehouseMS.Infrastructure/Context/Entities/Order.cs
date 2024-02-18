namespace WarehouseMS.Infrastructure.Context.Entities;

public class Order : BaseEntity
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int TrackingId { get; set; }
    public string OrderStatus { get; set; }
    public DateTime OrderSendDate { get; set; }
    public DateTime OrderReceivedDate { get; set; }
    public DateTime CreatedAt { get; set; }

    public User User { get; set; }
    public Product Product { get; set; }
}

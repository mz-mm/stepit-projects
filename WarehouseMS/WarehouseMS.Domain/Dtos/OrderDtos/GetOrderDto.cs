using WarehouseMS.Domain.Enums;

namespace WarehouseMS.Domain.Dtos.OrderDtos;

public class GetOrderDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int TrackingId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime OrderSendDate { get; set; }
    public DateTime OrderRecievedDate { get; set; }

}
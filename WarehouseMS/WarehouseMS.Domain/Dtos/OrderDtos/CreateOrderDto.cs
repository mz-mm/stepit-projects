namespace WarehouseMS.Domain.Dtos.OrderDtos;

public class CreateOrderDto
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int TrackingId { get; set; }
}
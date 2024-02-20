namespace WarehouseMS.Domain.Dtos.OrderDtos;

public class CreateOrderDto
{
    public int UserId { get; set; }
    public IEnumerable<int> ProductIds { get; set; }
}
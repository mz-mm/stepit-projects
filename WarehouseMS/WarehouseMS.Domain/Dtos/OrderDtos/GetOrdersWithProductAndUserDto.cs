using WarehouseMS.Domain.Dtos.ProductDtos;

namespace WarehouseMS.Domain.Dtos.OrderDtos;

public class GetOrdersWithProductAndUserDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int TrackingId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double Total { get; set; }
    public int OrderStatusId { get; set; }
    public string OrderStatus { get; set; }
    public List<string> OrderStatuses { get; set; } = new();
    public List<GetProductDto> Products { get; set; }
    public int ItemsCount { get; set; }
    public DateTime Date { get; set; }
}
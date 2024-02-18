namespace WarehouseMS.Domain.Dtos.ProductDtos;

public class GetProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public double Price { get; set; }
    public int StockQuantity { get; set; }
    public int StatusId { get; set; }
}
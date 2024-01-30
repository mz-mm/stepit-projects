namespace WarehouseMS.Domain.Dtos.ProductDtos;

public class CreateProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int StockQuantity { get; set; }
    public int CategoryId { get; set; }
}
﻿namespace WarehouseMS.Infrastructure.Context.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public double Price { get; set; }
    public int StockQuantity { get; set; }
    public int StatusId { get; set; }
    public StatusView StatusView { get; set; }
    public ICollection<OrderProduct> OrderProducts { get; set; }

    public DateTime CreatedAt { get; set; }
}

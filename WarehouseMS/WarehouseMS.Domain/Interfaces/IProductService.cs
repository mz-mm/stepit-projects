﻿using WarehouseMS.Domain.Dtos.ProductDtos;

namespace WarehouseMS.Domain.Interfaces;

public interface IProductService
{
    Task<IEnumerable<GetProductDto>> GetAllProductsAsync();
    Task<IEnumerable<GetProductWithStatusDto>> GetAllProductsWithStatusAsync();
    Task<IEnumerable<GetProductDto>> GetAllProductsByNameAsync(string productName);
    Task<GetProductDto> CreateProductAsync(CreateProductDto product);
    Task<GetProductDto?> GetProductByIdAsync(int productId);
    Task<bool> UpdateProductAsync(CreateProductDto product);
    Task<bool> DeleteProductAsync(int productId);
}
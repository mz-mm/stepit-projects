using AutoMapper;
using WarehouseMS.Domain.Attributes;
using WarehouseMS.Domain.Dtos.ProductDtos;
using WarehouseMS.Domain.Enums;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Infrastructure.Context.Entities;
using WarehouseMS.Infrastructure.Interfaces;

namespace WarehouseMS.Domain.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GetProductDto>>(products);
    }

    public async Task<IEnumerable<GetProductDto>> GetAllProductsByCategoryAsync(int categoryId)
    {
        var products = (await _productRepository.GetAllAsync()).Where(product => product.CategoryId == categoryId);
        return _mapper.Map<IEnumerable<GetProductDto>>(products);
    }

    public async Task<IEnumerable<GetProductDto>> GetAllProductsByNameAsync(string productName)
    {
        var products = (await _productRepository.GetAllAsync()).Where(product => product.Name == productName);
        return _mapper.Map<IEnumerable<GetProductDto>>(products);
    }

    public async Task<GetProductDto?> GetProductByIdAsync(int productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        return _mapper.Map<GetProductDto>(product);
    }

    public async Task<GetProductDto> CreateProductAsync(CreateProductDto product)
    {
        var productEntity = _mapper.Map<Product>(product);
        var result = await _productRepository.InsertAsync(productEntity);

        return _mapper.Map<GetProductDto>(result);
    }

    public async Task<bool> UpdateProductAsync(CreateProductDto product)
    {
        var productEntity = _mapper.Map<Product>(product);
        var result = await _productRepository.UpdateAsync(productEntity);

        return result;
    }

    public async Task<bool> DeleteProductAsync(int productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);

        if (product is null)
            return false;

        var result = await _productRepository.DeleteAsync(product);
        return result;
    }
}
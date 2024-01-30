using WarehouseMS.Domain.Dtos.CategoryDtos;

namespace WarehouseMS.Domain.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<GetCategoryDto>> GetAllCategoriesAsync();
    Task<GetCategoryDto?> GetCategoryByIdAsync(int categoryId);
    Task<GetCategoryDto> CreateCategoryAsync(CreateCategoryDto category);
    Task<bool> UpdateCategoryAsync(int categoryId, CreateCategoryDto category);
    Task<bool> DeleteCategoryAsync(int categoryId);
}
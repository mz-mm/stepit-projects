using AutoMapper;
using WarehouseMS.Domain.Dtos.CategoryDtos;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Infrastructure.Context.Entities;
using WarehouseMS.Infrastructure.Interfaces;

namespace WarehouseMS.Domain.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetCategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GetCategoryDto>>(categories);
    }

    public async Task<GetCategoryDto?> GetCategoryByIdAsync(int categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        return _mapper.Map<GetCategoryDto>(category);
    }

    public async Task<GetCategoryDto> CreateCategoryAsync(CreateCategoryDto category)
    {
        var categoryEntity = _mapper.Map<Category>(category);
        var result = await _categoryRepository.InsertAsync(categoryEntity);

        return _mapper.Map<GetCategoryDto>(result);
    }

    public async Task<bool> UpdateCategoryAsync(CreateCategoryDto category)
    {
        var categoryEntity = _mapper.Map<Category>(category);
        var result = await _categoryRepository.UpdateAsync(categoryEntity);

        return result;
    }

    public async Task<bool> DeleteCategoryAsync(int categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);

        if (category is null)
            return false;

        var result = await _categoryRepository.DeleteAsync(category);

        return result;
    }
}
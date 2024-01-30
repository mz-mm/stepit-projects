using AutoMapper;
using WarehouseMS.Domain.Dtos.CategoryDtos;
using WarehouseMS.Infrastructure.Context.Entities;

namespace WarehouseMS.Domain.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, GetCategoryDto>();
        CreateMap<CreateCategoryDto, Category>();
    } 
}
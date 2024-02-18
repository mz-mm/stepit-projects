using AutoMapper;
using WarehouseMS.Domain.Dtos.StatusDtos;
using WarehouseMS.Infrastructure.Context.Entities;

namespace WarehouseMS.Domain.Profiles;

public class StatusViewProfile : Profile
{
    public StatusViewProfile()
    {
        CreateMap<StatusView, GetStatusViewDto>();
        CreateMap<CreateStatusViewDto, StatusView>();
    }
}
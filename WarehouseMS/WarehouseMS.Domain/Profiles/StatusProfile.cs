using AutoMapper;
using WarehouseMS.Domain.Dtos.StatusDtos;
using WarehouseMS.Infrastructure.Context.Entities;

namespace WarehouseMS.Domain.Profiles;

public class StatusProfile : Profile
{
    public StatusProfile()
    {
        CreateMap<Status, GetStatusDto>();
        CreateMap<CreateStatusDto, Status>();
    }
}
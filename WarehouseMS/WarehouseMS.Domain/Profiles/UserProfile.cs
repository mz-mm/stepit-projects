using AutoMapper;
using WarehouseMS.Domain.Dtos.UserDtos;
using WarehouseMS.Domain.Resolvers;
using WarehouseMS.Infrastructure.Context.Entities;

namespace WarehouseMS.Domain.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, GetUserDto>().ForMember(dest => dest.Role, opt => opt.MapFrom<StringToUserRoleResolver>());
        CreateMap<CreateUserDto, User>().ForMember(dest => dest.Role, opt => opt.MapFrom<UserRoleToStringResolver>());
        CreateMap<LoginUserDto, User>();
    }
}
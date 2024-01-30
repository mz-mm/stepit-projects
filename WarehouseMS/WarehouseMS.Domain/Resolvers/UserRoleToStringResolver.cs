using AutoMapper;
using WarehouseMS.Domain.Dtos.UserDtos;
using WarehouseMS.Infrastructure.Context.Entities;

namespace WarehouseMS.Domain.Resolvers;

public class UserRoleToStringResolver : IValueResolver<CreateUserDto, User, string>
{
    public string Resolve(CreateUserDto src, User dest, string destMember, ResolutionContext context)
    {
        return src.Role.ToString();
    } 
}
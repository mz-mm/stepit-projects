using AutoMapper;
using WarehouseMS.Domain.Dtos.UserDtos;
using WarehouseMS.Domain.Enums;
using WarehouseMS.Infrastructure.Context.Entities;

namespace WarehouseMS.Domain.Resolvers;

public class StringToUserRoleResolver : IValueResolver<User, GetUserDto, UserRole>
{
    public UserRole Resolve(User src, GetUserDto dest, UserRole destMember, ResolutionContext context)
    {
        if (Enum.TryParse(src.Role, out UserRole result))
        {
            return result;
        }

        throw new InvalidOperationException($"Role '{src.Role}' cannot be mapped to UserRole enum.");
    }
}
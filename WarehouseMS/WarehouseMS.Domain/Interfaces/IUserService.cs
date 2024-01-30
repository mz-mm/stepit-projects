using WarehouseMS.Domain.Dtos.UserDtos;

namespace WarehouseMS.Domain.Interfaces;

public interface IUserService
{
    Task<IEnumerable<GetUserDto>> GetAllUsersAsync();
    Task<GetUserDto?> GetUserByIdAsync(int userId);
    Task<GetUserDto> CreateUserAsync(CreateUserDto user);
    Task<bool> UpdateUserAsync(int userId, CreateUserDto user);
    Task<bool> DeleteUserAsync(int userId);
}
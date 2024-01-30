using WarehouseMS.Domain.Dtos.UserDtos;
using WarehouseMS.Domain.Enums;

namespace WarehouseMS.Domain.Interfaces;

public interface IAuthService
{
    Task<string> LoginAsync(LoginUserDto userCredentials);
    Task<bool> IsAuthenticatedAsync();
    Task<bool> IsRole(UserRole userRole);
    Task LogoutAsync();
}
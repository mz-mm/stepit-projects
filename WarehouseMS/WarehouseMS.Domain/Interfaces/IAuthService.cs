using WarehouseMS.Domain.Dtos.UserDtos;
using WarehouseMS.Domain.Enums;

namespace WarehouseMS.Domain.Interfaces;

public interface IAuthService
{
    Task<bool> LoginAsync(LoginUserDto userCredentials);
    bool IsAuthenticatedAsync();
    bool IsUserRole(UserRole userRole);
    void LogoutAsync();
}
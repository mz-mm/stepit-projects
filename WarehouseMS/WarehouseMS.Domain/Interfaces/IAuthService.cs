using WarehouseMS.Domain.Dtos.UserDtos;
using WarehouseMS.Domain.Enums;

namespace WarehouseMS.Domain.Interfaces;

public interface IAuthService
{
    Task<bool> LoginAsync(LoginUserDto userCredentials);
    Task<bool> IsAuthenticatedAsync();
    Task<bool> CheckUserRole(UserRole userRole);
    void LogoutAsync();
}
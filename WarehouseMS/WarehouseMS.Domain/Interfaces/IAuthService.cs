using WarehouseMS.Domain.Dtos.UserDtos;
using WarehouseMS.Domain.Enums;

namespace WarehouseMS.Domain.Interfaces;

public interface IAuthService
{
    GetUserDto? UserIdentity { get; }
    Task<bool> LoginAsync(LoginUserDto userCredentials);
    bool IsAuthenticated();
    bool IsUserRole(UserRole userRole);
    void Logout();
}
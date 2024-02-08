using System.Security.Cryptography;
using WarehouseMS.Domain.Dtos.UserDtos;
using WarehouseMS.Domain.Enums;
using WarehouseMS.Domain.EventArgs;

namespace WarehouseMS.Domain.Interfaces;

public interface IAuthService
{
    GetUserDto? UserIdentity { get; }
    Task<bool> LoginAsync(LoginUserDto userCredentials);
    bool IsAuthenticated();
    bool IsUserRole(UserRole userRole);
    void Logout();
    void SubscribeUserLoggedIn(EventHandler<UserLoggedInEventArgs> handler);
    void UnsubscribeUserLoggedIn(EventHandler<UserLoggedInEventArgs> handler);
}
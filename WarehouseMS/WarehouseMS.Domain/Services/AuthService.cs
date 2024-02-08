using System.Security.Authentication;
using WarehouseMS.Domain.Dtos.UserDtos;
using WarehouseMS.Domain.Enums;
using WarehouseMS.Domain.EventArgs;
using WarehouseMS.Domain.Exceptions;
using WarehouseMS.Domain.Interfaces;

namespace WarehouseMS.Domain.Services;

public class AuthService : IAuthService
{
    public GetUserDto? UserIdentity { get; private set; }
    private readonly IUserService _userService;
    private EventHandler<UserLoggedInEventArgs>? _userLoggedIn;

    public AuthService(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> LoginAsync(LoginUserDto userCredentials)
    {
        var user = await _userService.GetUserByEmailAsync(userCredentials.Email);

        if (user is null)
            throw new InvalidCredentialException("Incorrect email or password");

        if (BCrypt.Net.BCrypt.Verify(user.Password, BCrypt.Net.BCrypt.HashPassword(userCredentials.Password)))
            throw new InvalidCredentialException("Incorrect email or password");

        UserIdentity = user;
        _userLoggedIn?.Invoke(this, new UserLoggedInEventArgs(UserIdentity));

        return true;
    }

    public bool IsAuthenticated()
    {
        if (UserIdentity is null)
            throw new NotAuthenticatedException();

        return true;
    }

    public bool IsUserRole(UserRole userRole)
    {
        if (UserIdentity is null)
            throw new NotAuthenticatedException();

        return userRole switch
        {
            UserRole.Any => true, // All users
            UserRole.Admin => UserIdentity.Role is UserRole.Manager or UserRole.SystemAdmin, // Users of admin type
            _ => UserIdentity.Role == userRole
        };
    }

    public void Logout()
    {
        UserIdentity = null;
        _userLoggedIn?.Invoke(this, new UserLoggedInEventArgs(null));
    }

    public void SubscribeUserLoggedIn(EventHandler<UserLoggedInEventArgs> handler)
    {
        _userLoggedIn += handler;
    }

    public void UnsubscribeUserLoggedIn(EventHandler<UserLoggedInEventArgs> handler)
    {
        _userLoggedIn -= handler;
    }
}
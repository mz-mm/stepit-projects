using System.Security.Authentication;
using GalaSoft.MvvmLight.Messaging;
using WarehouseMS.Domain.Dtos.UserDtos;
using WarehouseMS.Domain.Enums;
using WarehouseMS.Domain.Exceptions;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Domain.Messages;

namespace WarehouseMS.Domain.Services;

public class AuthService : IAuthService
{
    public GetUserDto? UserIdentity { get; private set; }
    private readonly IUserService _userService;
    private readonly IMessenger _messenger;

    public AuthService(IUserService userService, IMessenger messenger)
    {
        _userService = userService;
        _messenger = messenger;

        _messenger.Send(new UserLoginMessage
        {
            User = null
        });
    }

    public async Task<bool> LoginAsync(LoginUserDto userCredentials)
    {
        var user = await _userService.GetUserByEmailAsync(userCredentials.Email);

        if (user is null) throw new InvalidCredentialException("Incorrect email or password");

        if (!BCrypt.Net.BCrypt.Verify(userCredentials.Password, user.Password))
            throw new InvalidCredentialException("Incorrect email or password");

        UserIdentity = user;

        _messenger.Send(new UserLoginMessage
        {
            User = user
        });

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

        _messenger.Send(new UserLoginMessage
        {
            User = UserIdentity
        });
    }
}
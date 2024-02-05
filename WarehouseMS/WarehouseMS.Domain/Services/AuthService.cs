using System.Security.Authentication;
using WarehouseMS.Domain.Dtos.UserDtos;
using WarehouseMS.Domain.Enums;
using WarehouseMS.Domain.Exceptions;
using WarehouseMS.Domain.Interfaces;

namespace WarehouseMS.Domain.Services;

public class AuthService : IAuthService
{
    private GetUserDto? _userIdentity;
    private readonly IUserService _userService;

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

        _userIdentity = user;

        return true;
    }

    public bool IsAuthenticatedAsync()
    {
        if (_userIdentity is null)
            throw new NotAuthenticatedException();

        return true;
    }

    public bool IsUserRole(UserRole userRole)
    {
        if (_userIdentity is null)
            throw new NotAuthenticatedException();

        return userRole switch
        {
            UserRole.Any => true, // All users
            UserRole.Admin => _userIdentity.Role is UserRole.Manager or UserRole.SystemAdmin, // Users of admin type
            _ => _userIdentity.Role == userRole
        };
    }

    public void LogoutAsync()
    {
        _userIdentity = null;
    }
}
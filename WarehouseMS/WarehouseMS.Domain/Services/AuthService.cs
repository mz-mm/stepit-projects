using WarehouseMS.Domain.Dtos.UserDtos;
using WarehouseMS.Domain.Enums;
using WarehouseMS.Domain.Exceptions;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Infrastructure.Context.Entities;

namespace WarehouseMS.Domain.Services;

public class AuthService : IAuthService
{
    private GetUserDto? _userIdentity;
    private readonly UserService _userService;

    public AuthService(UserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> LoginAsync(LoginUserDto userCredentials)
    {
        var user = await _userService.GetUserByEmailAsync(userCredentials.Email);

        if (user is null)
            throw new ArgumentNullException(nameof(user));

        if (BCrypt.Net.BCrypt.Verify(user.Password, BCrypt.Net.BCrypt.HashPassword(userCredentials.Password)))
            throw new PasswordNotMatchException();

        _userIdentity = user;

        return true;
    }

    public bool IsAuthenticatedAsync()
    {
        if (_userIdentity is null)
            throw new NotAuthenticatedException();

        return true;
    }

    public bool CheckUserRole(UserRole userRole)
    {
        if (_userIdentity is null)
            throw new NotAuthenticatedException();

        return _userIdentity.Role == userRole;
    }

    public void LogoutAsync()
    {
        _userIdentity = null;
    }
}
using WarehouseMS.Domain.Dtos.UserDtos;
using WarehouseMS.Domain.Enums;
using WarehouseMS.Domain.Exceptions;
using WarehouseMS.Domain.Interfaces;

namespace WarehouseMS.Domain.Services;

public class AuthService : IAuthService
{
    private GetUserDto _userIdentity;
    private readonly UserService _userService;

    public async Task<bool> LoginAsync(LoginUserDto userCredentials)
    {
        var user = await _userService.GetUserByEmailAsync(userCredentials.Email);

        if (user is null)
            throw new ArgumentNullException(nameof(user));

        var doesPasswordsMatch =
            BCrypt.Net.BCrypt.Verify(user.Password, BCrypt.Net.BCrypt.HashPassword(userCredentials.Password));

        if (doesPasswordsMatch == false)
            throw new PasswordNotMatchException();

        return true;
    }


    public async Task<bool> IsAuthenticatedAsync()
    {
        if (_userIdentity is null)
            throw new NotAuthenticatedException();

        return true;
    }

    public async Task<bool> CheckUserRole(UserRole userRole)
    {
        return _userIdentity.Role == userRole;
    }

    public void LogoutAsync()
    {
        _userIdentity = null;
    }
}
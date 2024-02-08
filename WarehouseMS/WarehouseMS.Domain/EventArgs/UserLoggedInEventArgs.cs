using WarehouseMS.Domain.Dtos.UserDtos;

namespace WarehouseMS.Domain.EventArgs;

public class UserLoggedInEventArgs : System.EventArgs
{
    public GetUserDto? User { get; set; }

    public UserLoggedInEventArgs(GetUserDto? user)
    {
        User = user;
    }
}
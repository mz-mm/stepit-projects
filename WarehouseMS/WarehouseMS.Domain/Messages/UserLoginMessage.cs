using WarehouseMS.Domain.Dtos.UserDtos;

namespace WarehouseMS.Domain.Messages;

public class UserLoginMessage
{
    public GetUserDto? User { get; set; }
}
using WarehouseMS.Domain.Enums;

namespace WarehouseMS.Domain.Dtos.UserDtos;

public class CreateUserDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
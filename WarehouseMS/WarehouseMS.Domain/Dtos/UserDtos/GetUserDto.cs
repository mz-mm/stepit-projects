using WarehouseMS.Domain.Enums;

namespace WarehouseMS.Domain.Dtos.UserDtos;

public class GetUserDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserRole Role { get; set; }
}
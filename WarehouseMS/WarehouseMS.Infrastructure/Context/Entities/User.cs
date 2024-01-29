namespace WarehouseMS.Infrastructure.Context.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string CreatedAt { get; set; }

    public ICollection<Warehouse> Warehouses { get; set; }
}

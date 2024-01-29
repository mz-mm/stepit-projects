using System.ComponentModel.DataAnnotations;

namespace WarehouseMS.Infrastructure.Context.Entities;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
}

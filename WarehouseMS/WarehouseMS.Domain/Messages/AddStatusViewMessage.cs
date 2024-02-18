using WarehouseMS.Domain.Dtos.StatusDtos;

namespace WarehouseMS.Domain.Messages;

public class AddStatusViewMessage
{
    public GetStatusViewDto StatusView { get; set; }
}
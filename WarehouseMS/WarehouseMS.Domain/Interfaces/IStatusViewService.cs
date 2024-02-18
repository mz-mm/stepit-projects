using WarehouseMS.Domain.Dtos.StatusDtos;

namespace WarehouseMS.Domain.Interfaces;

public interface IStatusViewService
{
    Task<IEnumerable<GetStatusViewDto>> GetAllStatusAsync();
    Task<GetStatusViewDto?> GetStatusByIdAsync(int statusId);
    Task<GetStatusViewDto> CreateStatusAsync(CreateStatusViewDto statusView);
    Task<bool> UpdateStatusAsync(CreateStatusViewDto statusView);
    Task<bool> DeleteStatusAsync(int statusId);
}
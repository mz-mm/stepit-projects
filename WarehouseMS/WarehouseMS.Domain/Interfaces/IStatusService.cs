using WarehouseMS.Domain.Dtos.StatusDtos;

namespace WarehouseMS.Domain.Interfaces;

public interface IStatusService
{
    Task<IEnumerable<GetStatusDto>> GetAllStatusAsync();
    Task<GetStatusDto?> GetStatusByIdAsync(int statusId);
    Task<GetStatusDto> CreateStatusAsync(CreateStatusDto status);
    Task<bool> UpdateStatusAsync(CreateStatusDto status);
    Task<bool> DeleteStatusAsync(int statusId);
}
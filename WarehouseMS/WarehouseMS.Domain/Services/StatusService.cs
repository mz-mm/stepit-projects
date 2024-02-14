using AutoMapper;
using WarehouseMS.Domain.Dtos.StatusDtos;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Infrastructure.Context.Entities;
using WarehouseMS.Infrastructure.Interfaces;

namespace WarehouseMS.Domain.Services;

public class StatusService : IStatusService
{
    private readonly IStatusRepository _statusRepository;
    private readonly IMapper _mapper;

    public StatusService(IMapper mapper, IStatusRepository statusRepository)
    {
        _mapper = mapper;
        _statusRepository = statusRepository;
    }

    public async Task<IEnumerable<GetStatusDto>> GetAllStatusAsync()
    {
        var statuses = await _statusRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GetStatusDto>>(statuses);
    }

    public async Task<GetStatusDto?> GetStatusByIdAsync(int statusId)
    {
        var status = await _statusRepository.GetByIdAsync(statusId);
        return _mapper.Map<GetStatusDto>(status);
    }

    public async Task<GetStatusDto> CreateStatusAsync(CreateStatusDto status)
    {
        var statusEntity = _mapper.Map<Status>(status);
        var result = await _statusRepository.InsertAsync(statusEntity);

        return _mapper.Map<GetStatusDto>(result);
    }

    public async Task<bool> UpdateStatusAsync(CreateStatusDto status)
    {
        var statusEntity = _mapper.Map<Status>(status);
        var result = await _statusRepository.UpdateAsync(statusEntity);

        return result;
    }

    public async Task<bool> DeleteStatusAsync(int statusId)
    {
        var status = await _statusRepository.GetByIdAsync(statusId);

        if (status is null)
            return false;

        var result = await _statusRepository.DeleteAsync(status);

        return result;
    }
}
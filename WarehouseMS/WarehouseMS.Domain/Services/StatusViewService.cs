using AutoMapper;
using WarehouseMS.Domain.Dtos.StatusDtos;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Infrastructure.Context.Entities;
using WarehouseMS.Infrastructure.Interfaces;

namespace WarehouseMS.Domain.Services;

public class StatusViewService : IStatusViewService
{
    private readonly IStatusViewRepository _statusViewRepository;
    private readonly IMapper _mapper;

    public StatusViewService(IMapper mapper, IStatusViewRepository statusViewRepository)
    {
        _mapper = mapper;
        _statusViewRepository = statusViewRepository;
    }

    public async Task<IEnumerable<GetStatusViewDto>> GetAllStatusAsync()
    {
        var statuses = await _statusViewRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GetStatusViewDto>>(statuses);
    }

    public async Task<GetStatusViewDto?> GetStatusByIdAsync(int statusId)
    {
        var status = await _statusViewRepository.GetByIdAsync(statusId);
        return _mapper.Map<GetStatusViewDto>(status);
    }

    public async Task<GetStatusViewDto> CreateStatusAsync(CreateStatusViewDto statusView)
    {
        var statusEntity = _mapper.Map<StatusView>(statusView);
        var result = await _statusViewRepository.InsertAsync(statusEntity);

        return _mapper.Map<GetStatusViewDto>(result);
    }

    public async Task<bool> UpdateStatusAsync(CreateStatusViewDto statusView)
    {
        var statusEntity = _mapper.Map<StatusView>(statusView);
        var result = await _statusViewRepository.UpdateAsync(statusEntity);

        return result;
    }

    public async Task<bool> DeleteStatusAsync(int statusId)
    {
        var status = await _statusViewRepository.GetByIdAsync(statusId);

        if (status is null)
            return false;

        var result = await _statusViewRepository.DeleteAsync(status);

        return result;
    }
}
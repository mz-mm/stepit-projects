using System;
using AutoMapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using WarehouseMS.Domain.Dtos.StatusDtos;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Domain.Messages;
using WarehouseMS.Presentation.Services;

namespace WarehouseMS.Presentation.ViewModels;

public class AddStatusViewModel : ViewModelBase
{
    private readonly IStatusViewService _statusViewService;
    private readonly IMessenger _messenger;
    private readonly IMapper _mapper;

    private string _viewName;

    public string ViewName
    {
        get => _viewName;
        set => Set(ref _viewName, value);
    }

    private bool _isOpen;

    public bool IsOpen
    {
        get => _isOpen;
        set => Set(ref _isOpen, value);
    }

    private string _error;

    public string Error
    {
        get => _error;
        set => Set(ref _error, value);
    }

    private string _errorVisibility;

    public string ErrorVisibility
    {
        get => _errorVisibility;
        set => Set(ref _errorVisibility, value);
    }

    public AddStatusViewModel(IStatusViewService statusViewService, IMessenger messenger, IMapper mapper)
    {
        _statusViewService = statusViewService;
        _messenger = messenger;
        _mapper = mapper;
    }

    public RelayCommand CancelCommand => new(() => { IsOpen = false; });

    public RelayCommand AddViewCommand => new(async () =>
    {
        try
        {
            await _statusViewService.CreateStatusAsync(new CreateStatusViewDto
            {
                Name = ViewName
            });

            _messenger.Send(new AddStatusViewMessage
            {
                StatusView = new GetStatusViewDto
                {
                    Name = ViewName
                }
            });

            IsOpen = false;
        }
        catch (Exception ex)
        {
            ErrorVisibility = "Visible";
            Error = ex.Message;
        }
    });
}
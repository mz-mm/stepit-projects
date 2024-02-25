using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using WarehouseMS.Domain.Dtos.UserDtos;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Presentation.Interfaces;
using WarehouseMS.Presentation.Services;

namespace WarehouseMS.Presentation.ViewModels;

public class ManageUsersViewModel : ViewModelBase
{
    private IUserService _userService;
    private INavigationService _navigationService;

    private ObservableCollection<GetUserDto> _userses = new();

    public ObservableCollection<GetUserDto> Users
    {
        get => _userses;
        set => Set(ref _userses, value);
    }

    public ManageUsersViewModel(IUserService userService, INavigationService navigationService)
    {
        _userService = userService;
        _navigationService = navigationService;
    }

    private async void InitializeAsync()
    {
        var users = await _userService.GetAllUsersAsync();
        Users = new ObservableCollection<GetUserDto>(users);
    }

    public RelayCommand AddManagerCommand => new(() => _navigationService.HomeNavigateTo<AddAdminViewModel>());
}
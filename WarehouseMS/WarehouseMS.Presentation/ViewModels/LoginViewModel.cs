using System;
using GalaSoft.MvvmLight;
using WarehouseMS.Domain.Dtos.UserDtos;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Presentation.Interfaces;
using WarehouseMS.Presentation.Services;
using WarehouseMS.Presentation.Validators;

namespace WarehouseMS.Presentation.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IAuthService _authService;

    private string _email;

    public string Email
    {
        get => _email;
        set => Set(ref _email, value);
    }

    private string _password;

    public string Password
    {
        get => _password;
        set
        { 
            Set(ref _password, value);
        }
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

    public LoginViewModel(INavigationService navigationService, IAuthService authService)
    {
        _navigationService = navigationService;
        _authService = authService;
    }

    public RelayCommand LoginCommand =>
        new(
            Login,
            () => !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password)
        );

    private async void Login()
    {
        try
        {
            FieldValidator.ValidateEmail(Email);
            FieldValidator.ValidatePassword(Password);

            await _authService.LoginAsync(new LoginUserDto { Email = Email, Password = Password });
        }
        catch (Exception ex)
        {
            Error = ex.Message;
            _errorVisibility = "Visible";
        }
    }

    public RelayCommand SignupCommand =>
        new(() => { _navigationService.NavigateTo<SignupViewModel>(); });
}
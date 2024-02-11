using System;
using System.Security.Authentication;
using GalaSoft.MvvmLight;
using WarehouseMS.Domain.Dtos.UserDtos;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Presentation.Interfaces;
using WarehouseMS.Presentation.Services;
using WarehouseMS.Presentation.Validators;

namespace WarehouseMS.Presentation.ViewModels;

public class SignupViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IUserService _userService;
    private readonly IAuthService _authService;


    private string _email;

    public string Email
    {
        get => _email;
        set => Set(ref _email, value);
    }

    private string _firstName;

    public string FirstName
    {
        get => _firstName;
        set => Set(ref _firstName, value);
    }

    private string _lastName;

    public string LastName
    {
        get => _lastName;
        set => Set(ref _lastName, value);
    }

    private string _password;

    public string Password
    {
        get => _password;
        set => Set(ref _password, value);
    }

    private string _confirmPassword;

    public string ConfirmPassword
    {
        get => _confirmPassword;
        set => Set(ref _confirmPassword, value);
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

    public SignupViewModel(INavigationService navigationService, IUserService userService, IAuthService authService)
    {
        _navigationService = navigationService;
        _userService = userService;
        _authService = authService;
    }

    public RelayCommand Signup =>
        new(SignupAuthUser,
            () =>
                !string.IsNullOrWhiteSpace(Email) &&
                !string.IsNullOrWhiteSpace(Password) &&
                !string.IsNullOrWhiteSpace(FirstName) &&
                !string.IsNullOrWhiteSpace(LastName)
        );

    private async void SignupAuthUser()
    {
        try
        {
            if (Password != ConfirmPassword)
                throw new InvalidCredentialException("Make sure to enter the same password");

            FieldValidator.ValidateEmail(Email, exMessage: "Invalid Email");
            FieldValidator.ValidatePassword(Password, exMessage: "Invalid Password");
            FieldValidator.ValidateName(FirstName, exMessage: "Invalid First Name");
            FieldValidator.ValidateName(LastName, exMessage: "Invalid Last Name");

            await _userService.CreateUserAsync(new CreateUserDto
            {
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Password = Password,
            });

            // Authenticate user upon sign up
            await _authService.LoginAsync(new LoginUserDto { Email = Email, Password = Password });
        }
        catch (Exception ex)
        {
            ErrorVisibility = "Visible";
            Error = ex.Message;
        }
    }

    public RelayCommand LoginCommand =>
        new(() => { _navigationService.NavigateTo<LoginViewModel>(); });
}
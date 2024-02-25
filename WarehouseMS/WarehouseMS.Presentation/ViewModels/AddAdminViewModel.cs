using GalaSoft.MvvmLight;
using System.Security.Authentication;
using System;
using WarehouseMS.Domain.Dtos.UserDtos;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Presentation.Interfaces;
using WarehouseMS.Presentation.Services;
using WarehouseMS.Presentation.Validators;

namespace WarehouseMS.Presentation.ViewModels;

public class AddAdminViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IUserService _userService;

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

    public AddAdminViewModel(INavigationService navigationService, IUserService userService)
    {
        _navigationService = navigationService;
        _userService = userService;
    }

    public RelayCommand NavigateManageUsersCommand =>
        new(() => _navigationService.HomeNavigateTo<ManageUsersViewModel>());

    public RelayCommand CreateManagerCommand =>
        new(CreateManager,
            () =>
                !string.IsNullOrWhiteSpace(Email) &&
                !string.IsNullOrWhiteSpace(Password) &&
                !string.IsNullOrWhiteSpace(FirstName) &&
                !string.IsNullOrWhiteSpace(LastName) &&
                !string.IsNullOrWhiteSpace(Password) &&
                !string.IsNullOrWhiteSpace(ConfirmPassword)
        );

    private async void CreateManager()
    {
        try
        {
            if (Password != ConfirmPassword)
                throw new InvalidCredentialException("Make sure to enter the same password");

            FieldValidator.ValidateEmail(Email, exMessage: "Invalid Email");
            FieldValidator.ValidatePassword(Password, exMessage: "Invalid Password");
            FieldValidator.ValidateName(FirstName, exMessage: "Invalid First Name");
            FieldValidator.ValidateName(LastName, exMessage: "Invalid Last Name");

            await _userService.CreateManagerAsync(new CreateUserDto
            {
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Password = Password,
            });

            _navigationService.HomeNavigateTo<ManageUsersViewModel>();
        }
        catch (Exception ex)
        {
            ErrorVisibility = "Visible";
            Error = ex.Message;
        }
    }
}
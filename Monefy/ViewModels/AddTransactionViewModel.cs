using Monefy.Enums;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using Monefy.Models;

namespace Monefy.ViewModels;

public class AddTransactionViewModel : ViewModelBase, INotifyPropertyChanged
{

    private readonly INavigationService _navigationService;
    private readonly ITransactionsService _transactionsService;


    private string _description;
    public string Description
    {
        get => _description;
        set => Set(ref _description, value);
    }

    private string _amount;
    public string Amount
    {
        get => _amount;
        set => Set(ref _amount, value);
    }

    private bool _isExpenseChecked;
    public bool IsExpenseChecked
    {
        get => _isExpenseChecked; 
        set
        {
            if (_isExpenseChecked != value)
            {
                _isExpenseChecked = value;
                OnPropertyChanged(nameof(IsExpenseChecked));
                OnPropertyChanged(nameof(IsIncomeChecked));

                UpdateAvailableCategories();
            }
        }
    }

    private bool _isIncomeChecked;
    public bool IsIncomeChecked
    {
        get { return _isIncomeChecked; }
        set
        {
            if (_isIncomeChecked != value)
            {
                _isIncomeChecked = value;
                OnPropertyChanged(nameof(IsIncomeChecked));
                OnPropertyChanged(nameof(IsExpenseChecked));

                UpdateAvailableCategories();
            }
        }
    }

    private string? _selectedCategory;
    public string SelectedCategory
    {
        get { return _selectedCategory; }
        set
        {
            if (_selectedCategory != value)
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }
    }

    private List<string> _availableCategories;
    public List<string> AvailableCategories
    {
        get { return _availableCategories; }
        set
        {
            if (_availableCategories != value)
            {
                _availableCategories = value;
                OnPropertyChanged(nameof(AvailableCategories));
            }
        }
    }

    public AddTransactionViewModel(INavigationService navigationService, ITransactionsService transactionsService)
    {
        _navigationService = navigationService;
        _transactionsService = transactionsService;

        UpdateAvailableCategories();
    }

    private void UpdateAvailableCategories()
    {
        if (IsExpenseChecked)
        {
            AvailableCategories = Expense.GetCategories().ToList();
        }
        else if (IsIncomeChecked)
        {
            AvailableCategories = Income.GetCategories().ToList();
        }
        else
        {
            // Should set it not clickable
            AvailableCategories = new List<string>();
        }

        SelectedCategory = null;
    }

    public RelayCommand AddTransactionCommand
    {
        get => new(
        () =>
        {
            if (IsIncomeChecked)
            {
                _transactionsService.AddTransaction(
                    new Transaction(
                        description: Description,
                        amount: Convert.ToDouble(Amount),
                        category: EnumService.TryParseString<IncomeCategory>(SelectedCategory)
                    )
                );
            }
            else if (IsExpenseChecked)
            {
                _transactionsService.AddTransaction(
                    new Transaction(
                        description: Description,
                        amount: Convert.ToDouble(Amount),
                        category: EnumService.TryParseString<ExpenseCategory>(SelectedCategory)
                    )
                );
            }

            Description = null;
            Amount = null;
            IsIncomeChecked = false;
            IsExpenseChecked = false;
            SelectedCategory = null;

            _navigationService.NavigateTo<TransactionsViewModel>();
        },
        () =>
        {
            return 
                !string.IsNullOrEmpty( Description ) &&
                Convert.ToDouble(Amount) > 0 &&
                (IsExpenseChecked || IsIncomeChecked) &&
                !string.IsNullOrEmpty(SelectedCategory);
        }
        );
    }

    public RelayCommand CancelCommand
    {
        get => new(() =>
        {
            Description = null;
            Amount = null;
            IsIncomeChecked = false;
            IsExpenseChecked = false;
            SelectedCategory = null;

            _navigationService.NavigateTo<TransactionsViewModel>();
        });
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

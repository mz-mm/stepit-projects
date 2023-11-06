using GalaSoft.MvvmLight;
using Monefy.Models.Interfaces;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.ViewModels;
public class HomeViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private ObservableCollection<ITransaction> _transactions;

    public ObservableCollection<ITransaction> Transactions
    {
        get => _transactions;
        set
        {
            Set(ref _transactions, value);
        }
    }


    private double _currentBalance;
    public double CurrentBalance
    {
        get => _currentBalance;
        set => Set(ref _currentBalance, value);
    }

    private double _currentIncome;
    public double CurrentIncome
    {
        get => _currentIncome;
        set => Set(ref _currentIncome, value);
    }

    private double _currentExpense;
    public double CurrentExpense 
    {
        get => _currentExpense;
        set => Set(ref _currentExpense, value);
    }

    public HomeViewModel(ITransactionsService transactions, INavigationService navigationService)
    {
        Transactions = transactions.Transactions;
        _navigationService = navigationService;
    }

    public ButtonCommand AddTransactionCommand
    {
        get => new(() =>
        {
            _navigationService.NavigateTo<AddTransactionViewModel>();
        });
    }
}

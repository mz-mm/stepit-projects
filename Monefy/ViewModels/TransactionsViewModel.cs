using GalaSoft.MvvmLight;
using Monefy.Enums;
using Monefy.Models;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Monefy.ViewModels;
public class TransactionsViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly ITransactionsService _transactionsService;

    private TransactionType? _currentTransactionFilter;

    private ObservableCollection<Transaction>? _transactions;
    public ObservableCollection<Transaction> Transactions
    {
        get => _transactions;
        set
        {
            Set(ref _transactions, value);
        }
    }

    private string _clearButtonVisibility = "Hidden";
    public string ClearButtonVisibility
    {
        get => _clearButtonVisibility;
        set => Set(ref _clearButtonVisibility, value);
    }

    private double _currentBalance;
    public double CurrentBalance
    {
        // Income amount - expense amount
        get => _transactionsService.GetAllIncomeTransactions().Sum(t => t.Amount) - _transactionsService.GetAllExepenseTransaction().Sum(t => t.Amount);
        set => Set(ref _currentBalance, value);
    }

    private double _currentIncome;
    public double CurrentIncome
    {
        get => _transactionsService.GetAllIncomeTransactions().Sum(t => t.Amount);
        set => Set(ref _currentIncome, value);
    }

    private double _currentExpense;
    public double CurrentExpense
    {
        get => _transactionsService.GetAllExepenseTransaction().Sum(t => t.Amount);
        set => Set(ref _currentExpense, value);
    }

    private string? _searchQuery;
    public string SearchQuery
    {
        get => _searchQuery;
        set
        {
            Set(ref _searchQuery, value);
            UpdateFilteredTransactions();
        }
    }

    public TransactionsViewModel(ITransactionsService transactionsService, INavigationService navigationService)
    {
        _transactionsService = transactionsService;
        _navigationService = navigationService;
        Transactions = _transactionsService.Transactions;
    }

    private void UpdateFilteredTransactions()
    {
        IEnumerable<Transaction> filteredTransactions;

        if (!string.IsNullOrWhiteSpace(SearchQuery))
        {
            switch (_currentTransactionFilter)
            {
                case TransactionType.Income:
                    filteredTransactions = _transactionsService.GetAllIncomeTransactions().Where(t => t.Description.ToLower().Contains(SearchQuery));
                    break;
                case TransactionType.Expense:
                    filteredTransactions = _transactionsService.GetAllExepenseTransaction().Where(t => t.Description.ToLower().Contains(SearchQuery));
                    break;
                default:
                    filteredTransactions = _transactionsService.Transactions.Where(t => t.Description.ToLower().Contains(SearchQuery));
                    break;
            }
        }
        else
        {
            switch (_currentTransactionFilter)
            {
                case TransactionType.Income:
                    filteredTransactions = _transactionsService.GetAllIncomeTransactions();
                    break;

                case TransactionType.Expense:
                    filteredTransactions = _transactionsService.GetAllExepenseTransaction();
                    break;
                default:
                    filteredTransactions = _transactionsService.Transactions;
                    break;
            }
        }

        Transactions = new ObservableCollection<Transaction>(filteredTransactions);
    }


    public RelayCommand GetIncomeTransactions
    {
        get => new(() =>
        {
            _currentTransactionFilter = TransactionType.Income;

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                UpdateFilteredTransactions();
            }
            else
            {
                Transactions = _transactionsService.GetAllIncomeTransactions();
            }

            ClearButtonVisibility = "Visible";
        });
    }

    public RelayCommand GetExpenseTransactions
    {
        get => new(() =>
        {
            _currentTransactionFilter = TransactionType.Expense;

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                UpdateFilteredTransactions();
            }
            else
            {
                Transactions = _transactionsService.GetAllExepenseTransaction();
            }

            ClearButtonVisibility = "Visible";
        });
    }

    public RelayCommand ClearCommand
    {
        get => new(() =>
        {
            SearchQuery = "";
            _currentTransactionFilter = null;
            Transactions = _transactionsService.Transactions;
            ClearButtonVisibility = "Hidden";
        });
    }

    public RelayCommand AddTransactionCommand
    {
        get => new(() =>
        {
            _navigationService.NavigateTo<AddTransactionViewModel>();
        });
    }
}

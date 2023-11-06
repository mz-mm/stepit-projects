using MaterialDesignThemes.Wpf;
using Monefy.Enums;
using Monefy.Models;
using Monefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Monefy.Services.Classes;

public class TransactionService : ITransactionsService
{
    private IDeserializeService _deserializeService;
    private ISerializeService _serializeService;

    private ObservableCollection<Transaction> _transactions;
    public ObservableCollection<Transaction> Transactions
    {
        get => _transactions;
        set
        {
            _transactions = value;
            OnPropertyChanged(nameof(Transactions));
        }
    }
    private const string TransactionDatabasePath = "TransactionDB.json";

    public TransactionService(IDeserializeService deserializeService, ISerializeService serializeService)
    {
        _deserializeService = deserializeService;
        _serializeService = serializeService;

        _transactions = _deserializeService.Deserialize<Transaction>(TransactionDatabasePath) ?? new ObservableCollection<Transaction>();
    }

    public Transaction AddTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
        _serializeService.Serialize(TransactionDatabasePath, _transactions);

        return transaction;
    }

    public Transaction RemoveTransaction(Transaction transaction)
    {
        _transactions.Remove(transaction);
        _serializeService.Serialize(TransactionDatabasePath, _transactions);

        return transaction;
    }

    public ObservableCollection<Transaction> GetAllTransactions()
    {
     
        return new ObservableCollection<Transaction>(_transactions.OrderBy(t => t.Date));
    }

    public List<Transaction> GetAllExepenseTransaction()
    {
        var expenseTransactions = _transactions.Where(item =>
        {
            return Enum.TryParse(item.Category, out ExpenseCategory category);
        }).ToList();

        return expenseTransactions;
    }

    public List<Transaction> GetAllIncomeTransactions()
    {
        var incomeTransactions = _transactions.Where(item =>
        {
            return Enum.TryParse(item.Category, out ExpenseCategory category);
        }).ToList();

        return incomeTransactions;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

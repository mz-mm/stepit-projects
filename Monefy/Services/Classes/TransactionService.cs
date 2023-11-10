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
            OnPropertyChanged(nameof(Transaction));
        }
    }
    private const string TransactionDatabasePath = "TransactionDB.json";

    public TransactionService(IDeserializeService deserializeService, ISerializeService serializeService)
    {
        _deserializeService = deserializeService;
        _serializeService = serializeService;

        _transactions = _deserializeService.Deserialize<Transaction>(TransactionDatabasePath) ?? new ObservableCollection<Transaction>();
        _transactions = new ObservableCollection<Transaction>(Transactions.OrderBy(t => t.Date));
    }

    public Transaction AddTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);

        var sortedTransactions = new ObservableCollection<Transaction>(_transactions.OrderBy(t => t.Date));

        _transactions.Clear();

        foreach (var sortedTransaction in sortedTransactions)
        {
            _transactions.Add(sortedTransaction);
        }

        _serializeService.Serialize(TransactionDatabasePath, _transactions);

        OnPropertyChanged(nameof(Transactions));

        return transaction;
    }

    public Transaction RemoveTransaction(Transaction transaction)
    {
        Transactions.Remove(transaction);
        _serializeService.Serialize(TransactionDatabasePath, Transactions);

        return transaction;
    }

    public List<Transaction> GetAllExepenseTransaction()
    {
        var expenseTransactions = Transactions.Where(item =>
        {
            return Enum.TryParse(item.Category, out ExpenseCategory category);
        }).ToList();

        return expenseTransactions;
    }

    public List<Transaction> GetAllIncomeTransactions()
    {
        var incomeTransactions = Transactions.Where(item =>
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

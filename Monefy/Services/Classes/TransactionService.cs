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

        Transactions = _deserializeService.Deserialize<Transaction>(TransactionDatabasePath) ?? new ObservableCollection<Transaction>();
        Transactions = new ObservableCollection<Transaction>(Transactions.OrderBy(t => t.Date));
    }


    public Transaction AddTransaction(Transaction transaction)
    {
        Transactions.Add(transaction);

        var sortedTransactions = new ObservableCollection<Transaction>(Transactions.OrderBy(t => t.Date));

        Transactions.Clear();

        foreach (var sortedTransaction in sortedTransactions)
        {
            Transactions.Add(sortedTransaction);
        }

        _serializeService.Serialize(TransactionDatabasePath, Transactions);

        OnPropertyChanged(nameof(Transactions));

        return transaction;
    }

    public Transaction RemoveTransaction(Transaction transaction)
    {
        Transactions.Remove(transaction);
        _serializeService.Serialize(TransactionDatabasePath, Transactions);

        return transaction;
    }


    public ObservableCollection<Transaction> GetTransactionByInterval(Interval interval, DateTime date)
    {
        switch (interval)
        {
            case Interval.Days:
                return new ObservableCollection<Transaction>(Transactions.Where(t => t.Date.Day == date.Day));

            case Interval.Weeks:
                var startOfWeek = date.Date.AddDays(-(int)date.DayOfWeek);
                var endOfWeek = startOfWeek.AddDays(6);

                return new ObservableCollection<Transaction>(Transactions.Where(t => t.Date.Date >= startOfWeek && t.Date.Date <= endOfWeek));

            case Interval.Month:
                return new ObservableCollection<Transaction>(Transactions.Where(t => t.Date.Month == date.Month));

            default:
                return new ObservableCollection<Transaction>(Transactions.Where(t => t.Date.Day == date.Day));
        }
    }

    public ObservableCollection<Transaction> GetAllIncomeTransactions()
    {
        var incomeTransactions = new ObservableCollection<Transaction>
        (
            Transactions.Where(t => Enum.TryParse(t.Category, out IncomeCategory category))
        );


        return incomeTransactions;
    }

    public ObservableCollection<Transaction> GetAllExepenseTransaction()
    {
        var expenseTransactions = new ObservableCollection<Transaction>
        (
            Transactions.Where(t => Enum.TryParse(t.Category, out ExpenseCategory category))
        );

        return expenseTransactions;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

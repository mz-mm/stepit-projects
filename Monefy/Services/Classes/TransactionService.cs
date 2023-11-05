using Monefy.Models.Classes;
using Monefy.Models.Interfaces;
using Monefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Services.Classes;

public class TransactionService : ITransactionsService
{
    private IDeserializeService _deserializeService;
    private ISerializeService _serializeService;

    private ObservableCollection<ITransaction> _transactions;
    public ObservableCollection<ITransaction> Transactions
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

        _transactions = _deserializeService.Deserialize<ITransaction>(TransactionDatabasePath);
    }

    // Add a transaction
    public ITransaction AddTransaction(ITransaction transaction)
    {
        _transactions.Add(transaction);
        _serializeService.Serialize(TransactionDatabasePath, _transactions);

        return transaction;
    }

    // remove a transaction
    public ITransaction RemoveTransaction(ITransaction transaction)
    {
        _transactions.Remove(transaction);
        _serializeService.Serialize(TransactionDatabasePath, _transactions);

        return transaction;
    }

    // Get all transactions
    public ObservableCollection<ITransaction> GetAllTransactions()
    {
        var sortedTransactions = new ObservableCollection<ITransaction>(_transactions.OrderBy(t => t.Date));
        return sortedTransactions;
    }

    // Get all expense transactions
    public ObservableCollection<ExpenseTransaction> GetAllExepenseTransaction()
    {
        var expenseTransactions = new ObservableCollection<ExpenseTransaction>(_transactions.OfType<ExpenseTransaction>());
        return expenseTransactions;
    }


    // Get all income transactions
    public ObservableCollection<IncomeTransaction> GetAllIncomeTransactions()
    {
        var incomeTransactions = new ObservableCollection<IncomeTransaction>(_transactions.OfType<IncomeTransaction>());
        return incomeTransactions;
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

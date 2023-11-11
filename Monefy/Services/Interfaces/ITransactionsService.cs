using Monefy.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Monefy.Services.Interfaces;

public interface ITransactionsService
{
    public ObservableCollection<Transaction> Transactions { get; set; }

    // Add a transaction
    public Transaction AddTransaction(Transaction transaction);

    // remove a transaction
    public Transaction RemoveTransaction(Transaction transaction);

    // Get all expense transactions
    public ObservableCollection<Transaction> GetAllExepenseTransaction();

    // Get all income transactions
    public ObservableCollection<Transaction> GetAllIncomeTransactions();
}

using Monefy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Services.Interfaces;

public interface ITransactionsService
{
    public ObservableCollection<Transaction> Transactions { get; set; }

    // Add a transaction
    public Transaction AddTransaction(Transaction transaction);

    // remove a transaction
    public Transaction RemoveTransaction(Transaction transaction);

    // Get all expense transactions
    public List<Transaction> GetAllExepenseTransaction();

    // Get all income transactions
    public List<Transaction> GetAllIncomeTransactions();

}

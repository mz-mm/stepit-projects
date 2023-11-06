using Monefy.Models.Classes;
using Monefy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Services.Interfaces;

public interface ITransactionsService
{
    public ObservableCollection<ITransaction> Transactions { get; set; }

    // Add a transaction
    public ITransaction AddTransaction(ITransaction transaction);

    // remove a transaction
    public ITransaction RemoveTransaction(ITransaction transaction);

    // Get all transactions
    public ObservableCollection<ITransaction> GetAllTransactions();

    // Get all expense transactions
    public ObservableCollection<ExpenseTransaction> GetAllExepenseTransaction();

    // Get all income transactions
    public ObservableCollection<IncomeTransaction> GetAllIncomeTransactions();

}

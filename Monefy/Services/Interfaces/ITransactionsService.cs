using Monefy.Models.Classes;
using Monefy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Services.Interfaces;

public interface ITransactionsService
{
    // Add a transaction
    public ITransaction AddTransaction(ITransaction transaction);

    // remove a transaction
    public ITransaction RemoveTransaction(ITransaction transaction);

    // Get all transactions
    public List<ITransaction> GetAllTransactions();

    // Get all expense transactions
    public List<ExpenseTransaction> GetAllExepenseTransaction();

    // Get all income transactions
    public List<IncomeTransaction> GetAllIncomeTransactions();

}

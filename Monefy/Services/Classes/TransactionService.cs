using Monefy.Models.Classes;
using Monefy.Models.Interfaces;
using Monefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Services.Classes;

public class TransactionService : ITransactionsService
{
    private IDeserializeService _deserializeService;
    private ISerializeService _serializeService;

    private List<ITransaction> _transactions;
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
    public List<ITransaction> GetAllTransactions()
    {
        _transactions.Sort((transactionOne, transactionTwo) => transactionOne.Date.CompareTo(transactionTwo.Date));
        return _transactions;
    }

    // Get all expense transactions
    public List<ExpenseTransaction> GetAllExepenseTransaction()
    {
        return _transactions.OfType<ExpenseTransaction>().ToList();
    }

    // Get all income transactions
    public List<IncomeTransaction> GetAllIncomeTransactions()
    {
        return _transactions.OfType<IncomeTransaction>().ToList();
    }

}

using Monefy.Enums;
using Monefy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Monefy.Services.Interfaces;

public interface ITransactionsService
{
    public ObservableCollection<Transaction> Transactions { get; set; }

    public Transaction AddTransaction(Transaction transaction);

    public Transaction RemoveTransaction(Transaction transaction);

    public ObservableCollection<Transaction> GetTransactionByInterval(Interval interval, DateTime date);

    public ObservableCollection<Transaction> GetAllExepenseTransaction();

    public ObservableCollection<Transaction> GetAllIncomeTransactions();
}

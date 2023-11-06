using Monefy.Enums;
using Newtonsoft.Json;
using System;

namespace Monefy.Models;

public class Transaction
{
    public readonly TransactionType transactionType;
    public string Description { get; set; }
    public double Amount { get; set; }
    public DateTime Date { get; set; }
    public string Category { get; set; }

    [JsonConstructor]
    public Transaction(string description, double amount, string category)
    {
        transactionType = TransactionType.Expense;
        Description = description;
        Amount = amount;
        Date = DateTime.Now;
        Category = category;
    }

    // Transaction as Expense transaction
    public Transaction(string description, double amount, ExpenseCategory category)
    {
        transactionType = TransactionType.Expense;
        Description = description;
        Amount = amount;
        Date = DateTime.Now;
        Category = category.ToString();
    }

    // Transaction as Income transaction
    public Transaction(string description, double amount, IncomeCategory category)
    {
        transactionType = TransactionType.Income;
        Description = description;
        Amount = amount;
        Date = DateTime.Now;
        Category = category.ToString();
    }
}

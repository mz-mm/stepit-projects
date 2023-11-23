using Monefy.Enums;
using Newtonsoft.Json;
using System;

namespace Monefy.Models;

public class Transaction
{
    public readonly TransactionType TypeTransaction;
    public string Description { get; set; }
    public double Amount { get; set; }
    public DateTime Date { get; set; }
    public string Category { get; set; }
    public Icon Icon { get; set; }
    public string TransactionTypeIcon { get; set; }
    public string TransactionTypeColor { get; set; }

    [JsonConstructor]
    public Transaction(TransactionType transactionType, string description, DateTime date, double amount, string category, Icon icon, string transactionIcone, string transactionColor)
    {
        TypeTransaction = transactionType;
        Description = description;
        Amount = amount;
        Date = date;
        Category = category;
        Icon = icon;
        TransactionTypeIcon = transactionIcone;
        TransactionTypeColor = transactionColor;
    }

    // Transaction as Expense transaction
    public Transaction(string description, double amount, ExpenseCategory category)
    {
        TypeTransaction = TransactionType.Expense;
        Description = description;
        Amount = amount;
        Date = DateTime.Today;
        TransactionTypeIcon = Expense.TransactionTypeIcon;
        TransactionTypeColor = Expense.TransactionTypeColor;
        Category = category.ToString();
        Icon = Expense.GetIcon(category);
    }

    // Transaction as Income transaction
    public Transaction(string description, double amount, IncomeCategory category)
    {
        TypeTransaction = TransactionType.Income;
        Description = description;
        Amount = amount;
        Date = DateTime.Now;
        TransactionTypeIcon = Income.TransactionTypeIcon;
        TransactionTypeColor = Income.TransactionTypeColor;
        Category = category.ToString();
        Icon = Income.GetIcon(category);
    }



}

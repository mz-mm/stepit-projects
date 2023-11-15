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
    public string Icon { get; set; }
    public string TransactionTypeIcon { get; set; }
    public string TransactionTypeColor { get; set; }

    [JsonConstructor]
    public Transaction(TransactionType transactionType, string description, DateTime date, double amount, string category, string icon, string transactionIcone, string transactionColor)
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
        TransactionTypeIcon = "Minus";
        TransactionTypeColor = "Black";
        Category = category.ToString();

        switch (category)
        {
            case ExpenseCategory.Entertaiment:
                Icon = "Popcorn";
                break;

            case ExpenseCategory.Groceries:
                Icon = "Shopping";
                break;

            case ExpenseCategory.Rent:
                Icon = "Building";
                break;

            case ExpenseCategory.Utilities:
                Icon = "Wrench";
                break;

            case ExpenseCategory.Transportation:
                Icon = "Transportation";
                break;
        }
    }

    // Transaction as Income transaction
    public Transaction(string description, double amount, IncomeCategory category)
    {
        TypeTransaction = TransactionType.Income;
        Description = description;
        Amount = amount;
        Date = DateTime.Now;
        TransactionTypeIcon = "Plus";
        TransactionTypeColor = "#3CC64A";
        Category = category.ToString();

        switch (category)
        {
            case IncomeCategory.Salary:
                Icon = "Bank";
                break;

            case IncomeCategory.Investment:
                Icon = "TrendingUp";
                break;

            case IncomeCategory.Gift:
                Icon = "Gift";
                break;
        }
    }
}

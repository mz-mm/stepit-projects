using Monefy.Enums;
using Monefy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Models.Classes;

public class ExpenseTransaction : ITransaction
{
    public string Description { get; set; }
    public double Amount { get; set; }
    public DateTime Date { get; set; }
    public ExpenseCategory Category { get; set; }

    public ExpenseTransaction(string description, double amount, ExpenseCategory category)
    {
        Description = description;
        Amount = amount;
        Date = DateTime.Now;
        Category = category;
    }
}

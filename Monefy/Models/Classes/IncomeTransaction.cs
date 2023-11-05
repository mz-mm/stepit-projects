using Monefy.Models.Interfaces;
using Monefy.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Models.Classes;

public class IncomeTransaction : ITransaction
{ 
    public string Description { get; set; }
    public double Amount { get; set; }
    public DateTime Date { get; set; }
    public IncomeCategory Category { get; set; }

    public IncomeTransaction(string description, double amount, IncomeCategory category)
    {
        Description = description;
        Amount = amount;
        Date = DateTime.Now;
        Category = category;
    }
}

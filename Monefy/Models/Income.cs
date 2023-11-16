using Monefy.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monefy.Models;

public static class Income
{
    public static string TransactionTypeIcon = "Plus";
    public static string TransactionTypeColor = "#3CC64As";

    public static IncomeCategory TryParse(string category)
    {
        IncomeCategory incomeCategory;
        Enum.TryParse(category, out incomeCategory);

        return incomeCategory;
    }

    public static IEnumerable<string> GetCategories()
    {
        return Enum.GetValues(typeof(IncomeCategory))
                       .Cast<IncomeCategory>()
                       .Select(category => category.ToString());
    }

    public static string GetIcon(IncomeCategory category)
    {
        return category switch
        {
            IncomeCategory.Salary => "Bank",
            IncomeCategory.Investment => "TrendingUp",
            IncomeCategory.Gift => "Gift",
            _ => "Dot",
        };
    }
}

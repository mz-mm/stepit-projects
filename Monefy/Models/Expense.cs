using Monefy.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monefy.Models;


public class Expense
{
    public static string TransactionTypeIcon = "Minus";
    public static string TransactionTypeColor = "Black";

    public static ExpenseCategory TryParse(string category)
    {

        ExpenseCategory expenseCategory;
        Enum.TryParse(category, out expenseCategory);

        return expenseCategory;
    }

    public static IEnumerable<string> GetCategories()
    {
        return Enum.GetValues(typeof(ExpenseCategory))
                       .Cast<ExpenseCategory>()
                       .Select(category => category.ToString());
    }

    public static Icon GetIcon(ExpenseCategory category)
    {
        return category switch
        {
            ExpenseCategory.Rent => new Icon("Building", "#3498db"),
            ExpenseCategory.Utilities => new Icon("Wrench", "#e74c3c"),
            ExpenseCategory.Groceries => new Icon("Shopping", "#2ecc71"),
            ExpenseCategory.Clothing => new Icon("TshirtCrew", "#3498db"),
            ExpenseCategory.Transportation => new Icon("Transportation", "#f1c40f"),
            ExpenseCategory.Travel => new Icon("Flight", "#e74c3c"),
            ExpenseCategory.Entertaiment => new Icon("Popcorn", "#3498db"),
            ExpenseCategory.Pet => new Icon("Cat", "#e74c3c"),
            ExpenseCategory.Healthcare => new Icon("Heart", "#2ecc71"),
            ExpenseCategory.Education => new Icon("Book", "#3498db"),
            ExpenseCategory.Dining => new Icon("Silverware", "#808080"),
            ExpenseCategory.Gifts => new Icon("Gift", "#e67e22"),
            _ => new Icon("Category", "#95a5a6"),
        };
    }

    public static List<Category> GetCategoriesWithIcon()
    {
        return GetCategories() 
            .Select(category => new Category(category.ToString(), GetIcon(TryParse(category))))
            .ToList();
    }

}

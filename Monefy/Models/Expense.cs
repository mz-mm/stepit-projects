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

    public static string GetIcon(ExpenseCategory category)
    {
        return category switch
        {
            ExpenseCategory.Rent => "Building",
            ExpenseCategory.Utilities => "Wrench",
            ExpenseCategory.Groceries => "Shopping",
            ExpenseCategory.Clothing => "TshirtCrew",
            ExpenseCategory.Transportation => "Transportation",
            ExpenseCategory.Travel => "Flight",
            ExpenseCategory.Entertaiment => "Popcorn",
            ExpenseCategory.Pet => "Cat",
            ExpenseCategory.Healthcare => "Heart",
            ExpenseCategory.Education => "Book",
            ExpenseCategory.Dining => "Silverware",
            ExpenseCategory.Gifts => "Gift",
            _ => "Dot",
        };
    }

    public static List<Category> GetCategoriesWithIcon()
    {
        var categories = GetCategories();

        return categories
            .Select(category => new Category(category.ToString(), GetIcon(TryParse(category))))
            .ToList();
    }

}

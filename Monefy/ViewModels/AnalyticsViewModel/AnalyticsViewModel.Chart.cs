using LiveCharts;
using LiveCharts.Wpf;
using Monefy.Enums;
using Monefy.Models;
using Monefy.Services.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace Monefy.ViewModels;

public partial class AnalyticsViewModel
{
    private void UpdateChartData(ObservableCollection<Transaction> transactions, string? dateTime = null)
    {
        ChartData = new();
        var currentCategories = new List<ExpenseCategory>();

        foreach (var category in Expense.GetCategories())
        {
            var filteredTransactions = (dateTime != null)
                                     ? _transactionsService.GetTransactionByInterval(EnumService.TryParseString<Interval>(CurrentInterval), DateTime.Parse(dateTime)).Where(t => t.Category == category)
                                     : transactions.Where(t => t.Category == category);

            if (filteredTransactions.Any())
            {
                currentCategories.Add(EnumService.TryParseString<ExpenseCategory>(category));

                ChartData.Add(
                    new PieSeries
                    {
                        Title = category,
                        Values = new ChartValues<double> { filteredTransactions.Sum(t => t.Amount) },
                        Tag = filteredTransactions.Sum(t => t.Amount).ToString(),
                        Fill = Expense.GetIcon(EnumService.TryParseString<ExpenseCategory>(category)).Color,
                        DataLabels = true,
                    }
                );
            }
        }

        DataVisibily = ChartData.Any() ? "Hidden" : "Visible";

        if (dateTime != null && CurrentDate != dateTime)
        {
            CurrentDate = dateTime;
        }
    }
}

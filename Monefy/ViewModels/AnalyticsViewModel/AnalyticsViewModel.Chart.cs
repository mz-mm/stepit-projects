using LiveCharts;
using LiveCharts.Wpf;
using Monefy.Enums;
using Monefy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Monefy.ViewModels;

public partial class AnalyticsViewModel
{
    private SeriesCollection _chartData = new();
    public SeriesCollection ChartData
    {
        get => _chartData;
        set
        {
            Set(ref _chartData, value);
        }
    }

    private void UpdateChartData(ObservableCollection<Transaction> transactions, DateTime? dateTime = null)
    {
        ChartData?.Clear();
        var currentCategories = new List<ExpenseCategory>();

        foreach (var category in Expense.GetCategories())
        {
            var filteredTransactions = (dateTime.HasValue)
                                     ? transactions.Where(t => t.Category == category && t.Date == dateTime.Value)
                                     : transactions.Where(t => t.Category == category);

            if (filteredTransactions.Any())
            {
                currentCategories.Add(Expense.TryParse(category));

                ChartData?.Add(
                    new PieSeries
                    {
                        Title = category,
                        Values = new ChartValues<double> { filteredTransactions.Sum(t => t.Amount) },
                        Tag = filteredTransactions.Sum(t => t.Amount).ToString(),
                        Fill = Expense.GetIcon(Expense.TryParse(category)).Color,
                        DataLabels = true,
                    }
                );
            }
        }

        DataVisibily = ChartData.Any() ? "Hidden" : "Visible";

        if (dateTime.HasValue && CurrentDate != dateTime.Value)
        {
            CurrentDate = dateTime.Value;
        }
    }
}

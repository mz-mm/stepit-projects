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

namespace Monefy.ViewModels;

public partial class AnalyticsViewModel
{
    private SeriesCollection _chartData;
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
        if (_chartData == null)
        {
            _chartData = new SeriesCollection();
        }
        else
        {
            _chartData.Clear();
        }

        foreach (var category in Enum.GetValues(typeof(ExpenseCategory)).Cast<ExpenseCategory>().Select(category => category.ToString()))
        {
            var filteredTransactions = (dateTime.HasValue)
                                     ? transactions.Where(t => t.Category == category && t.Date == dateTime.Value)
                                     : transactions.Where(t => t.Category == category);

            if (filteredTransactions.Any())
            {
                ChartData.Add(
                    new PieSeries
                    {
                        Title = category,
                        Values = new ChartValues<double> { filteredTransactions.Sum(t => t.Amount) },
                        Tag = filteredTransactions.Sum(t => t.Amount).ToString(),
                        DataLabels = true
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

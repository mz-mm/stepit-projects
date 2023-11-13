using GalaSoft.MvvmLight;
using System;
using LiveCharts.Wpf;
using LiveCharts;
using Monefy.Services.Interfaces;
using Monefy.Enums;
using System.Linq;
using System.Collections.ObjectModel;
using Monefy.Models;

namespace Monefy.ViewModels;

public class AnalyticsViewModel : ViewModelBase
{
    public SeriesCollection ChartData { get; } = new SeriesCollection();

    public AnalyticsViewModel(ITransactionsService transactionsService)
    {
        UpdateChartData(transactionsService.GetAllExepenseTransaction());

        transactionsService.Transactions.CollectionChanged += (sender, e) =>
        {
            UpdateChartData(transactionsService.GetAllExepenseTransaction());
        };
    }

    private void UpdateChartData(ObservableCollection<Transaction> transactions)
    {
        ChartData.Clear();

        foreach (var category in Enum.GetValues(typeof(ExpenseCategory)).Cast<ExpenseCategory>().Select(category => category.ToString()))
        {
            var filteredTransactions = transactions.Where(t => t.Category == category);

            if (filteredTransactions.Any())
            {
                ChartData.Add(
                    new PieSeries
                    {
                        Title = category,
                        Values = new ChartValues<double> { filteredTransactions.Sum(t => t.Amount) },
                        LabelPoint = point => string.Format("{0:C}", point.Y)
                    }
                );
            }
        }
    }
}


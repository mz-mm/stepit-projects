using GalaSoft.MvvmLight;
using System;
using LiveCharts.Wpf;
using LiveCharts;
using Monefy.Services.Interfaces;
using Monefy.Enums;
using System.Linq;
using System.Collections.ObjectModel;
using Monefy.Models;
using Monefy.Services.Classes;

namespace Monefy.ViewModels;

public class AnalyticsViewModel : ViewModelBase
{
    private DateTime _currentDate;
    public DateTime CurrentDate 
    {
        get => _currentDate;
        set => Set(ref _currentDate, value);
    }

    public SeriesCollection ChartData { get; set; }

    public AnalyticsViewModel(ITransactionsService transactionsService)
    {
        CurrentDate = DateTime.Today;
        ChartData = new SeriesCollection();
        UpdateChartData(transactionsService.GetAllExepenseTransaction());

        transactionsService.Transactions.CollectionChanged += (sender, e) =>
        {
            UpdateChartData(transactionsService.GetAllExepenseTransaction());
        };
    }

    private void UpdateChartData(ObservableCollection<Transaction> transactions)
    {
        ChartData = new SeriesCollection();

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
                        Tag = filteredTransactions.Sum(t => t.Amount).ToString(),
                        DataLabels = true
                    }
                );
            }
        }
    }

    public RelayCommand DayBackCommand
    {
        get => new(() =>
        {
            CurrentDate.AddDays(-1);
        });
    }
    public RelayCommand DayForwardCommand
    {
        get => new(() =>
        {
            CurrentDate.AddDays(1);
        });
    }
}


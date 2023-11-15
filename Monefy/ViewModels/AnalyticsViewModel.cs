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
    private ITransactionsService _transactionsService;

    private string _dataVisibily;
    public string DataVisibily
    {
        get => _dataVisibily;
        set => Set(ref _dataVisibily, value);
    }

    private DateTime _currentDate;
    public DateTime CurrentDate 
    {
        get => _currentDate;
        set
        {
            Set(ref _currentDate, value);
            UpdateChartData(_transactionsService.GetAllExepenseTransaction(), _currentDate);
        }
    }

    private SeriesCollection _chartData;
    public SeriesCollection ChartData
    {
        get => _chartData;
        set
        {
            Set(ref _chartData, value);
        }
    }

    public AnalyticsViewModel(ITransactionsService transactionsService)
    {
        _transactionsService = transactionsService;

        CurrentDate = DateTime.Today;
        _chartData = new SeriesCollection();

        UpdateChartData(_transactionsService.GetAllExepenseTransaction(), DateTime.Today);

        transactionsService.Transactions.CollectionChanged += (sender, e) =>
        {
            UpdateChartData(_transactionsService.GetAllExepenseTransaction(), DateTime.Today);
        };
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

    public RelayCommand DayBackCommand
    {
        get => new(() =>
        {
            CurrentDate = CurrentDate.AddDays(-1);
        });
    }

    public RelayCommand DayForwardCommand
    {
        get => new(() =>
        {
            CurrentDate = CurrentDate.AddDays(1);
        });
    }

    public RelayCommand AllCommand
    {
        get => new(() => 
        {
            UpdateChartData(_transactionsService.GetAllExepenseTransaction());
        });
    }

    public RelayCommand TodayCommand
    {
        get => new(() =>
        {
            UpdateChartData(_transactionsService.GetAllExepenseTransaction(), DateTime.Today);
        });
    }
}


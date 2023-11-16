using GalaSoft.MvvmLight;
using System;
using LiveCharts;
using Monefy.Services.Interfaces;
using Monefy.Services.Classes;

namespace Monefy.ViewModels;

public partial class AnalyticsViewModel : ViewModelBase
{
    private ITransactionsService _transactionsService;

    private string _dataVisibily;
    public string DataVisibily
    {
        get => _dataVisibily;
        set
        {
            Set(ref _dataVisibily, value);
            CategoryVisibility = value == "Visible" ? "Hidden" : "Visible";
        }

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


    public AnalyticsViewModel(ITransactionsService transactionsService)
    {
        _transactionsService = transactionsService;

        CurrentDate = DateTime.Today;
        _chartData = new SeriesCollection();

        UpdateCategoryIcons();
        UpdateChartData(_transactionsService.GetAllExepenseTransaction(), DateTime.Today);

        transactionsService.Transactions.CollectionChanged += (sender, e) =>
        {
            UpdateChartData(_transactionsService.GetAllExepenseTransaction(), DateTime.Today);
        };
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


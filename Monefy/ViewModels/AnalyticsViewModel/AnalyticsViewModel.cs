using GalaSoft.MvvmLight;
using System;
using LiveCharts;
using Monefy.Services.Interfaces;
using Monefy.Services.Classes;
using System.Linq;
using System.Collections.Generic;
using Monefy.Enums;

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

    private double _currentExpense;
    public double CurrentExpense
    {
        get => _currentExpense;
        set => Set(ref _currentExpense, value);
    }

    private string _currentDate;
    public string CurrentDate
    {
        get => _currentDate;
        set
        {
            Set(ref _currentDate, value);
            UpdateChartData(_transactionsService.GetAllExepenseTransaction(), _currentDate);
        }
    }

    // Gets all intervals
    public List<string> Intervals { get; set; } = new(Enum.GetValues(typeof(Interval)).Cast<Interval>().Select(t => t.ToString()));

    private string _currentInterval = Interval.Days.ToString();
    public string CurrentInterval
    {
        get => _currentInterval;
        set
        {
            Set(ref _currentInterval, value);

            if (_currentInterval == Interval.Days.ToString())
            {
                CurrentDate = DateTime.Today.ToString("dd/MM/yyyy");
            }

            else if (_currentInterval == Interval.Weeks.ToString())
            {
                CurrentDate = DateTime.Today.ToString("dd/MM/yyyy");
            }

            else if (_currentInterval == Interval.Month.ToString())
            {
                CurrentDate = DateTime.Today.ToString("MM/yyyy");
            }

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


    private void UpdateCurrentExpense(string? dateTime = null)
    {
        if (dateTime == null)
        {
            CurrentExpense = _transactionsService.GetAllExepenseTransaction().Sum(t => t.Amount);
        }
        else
        {
            CurrentExpense = _transactionsService.GetAllExepenseTransaction().Where(t => t.Date == DateTime.Parse(dateTime)).Sum(t => t.Amount);
        }
    }

    public AnalyticsViewModel(ITransactionsService transactionsService)
    {
        _transactionsService = transactionsService;

        CurrentDate = DateTime.Today.ToString("dd/MM/yyyy");

        UpdateCategoryIcons();
        UpdateCurrentExpense(CurrentDate);

        UpdateChartData(_transactionsService.GetAllExepenseTransaction(), DateTime.Today.ToString("dd/MM/yyyy"));

        transactionsService.Transactions.CollectionChanged += (sender, e) =>
        {
            UpdateChartData(_transactionsService.GetAllExepenseTransaction(), DateTime.Today.ToString("dd/MM/yyyy"));
        };
    }

    public RelayCommand DayBackCommand
    {
        get => new(() =>
        {
            if (_currentInterval == Interval.Days.ToString())
            {
                CurrentDate = DateTime.Parse(CurrentDate).AddDays(-1).ToString("dd/MM/yyyy");
            }

            else if (_currentInterval == Interval.Weeks.ToString())
            {
                CurrentDate = DateTime.Parse(CurrentDate).AddDays(-7).ToString("dd/MM/yyyy");
            }

            else if (_currentInterval == Interval.Month.ToString())
            {
                CurrentDate = DateTime.Parse(CurrentDate).AddMonths(-1).ToString("MM/yyyy");
            }
            UpdateCurrentExpense(CurrentDate);
        });
    }

    public RelayCommand DayForwardCommand
    {
        get => new(() =>
        {
            if (_currentInterval == Interval.Days.ToString())
            {
                CurrentDate = DateTime.Parse(CurrentDate).AddDays(1).ToString("dd/MM/yyyy");
            }

            else if (_currentInterval == Interval.Weeks.ToString())
            {
                CurrentDate = DateTime.Parse(CurrentDate).AddDays(7).ToString("dd/MM/yyyy");
            }

            else if (_currentInterval == Interval.Month.ToString())
            {
                CurrentDate = DateTime.Parse(CurrentDate).AddMonths(1).ToString("MM/yyyy");
            }

            UpdateCurrentExpense(CurrentDate);
        });
    }

    public RelayCommand AllCommand
    {
        get => new(() =>
        {
            UpdateChartData(_transactionsService.GetAllExepenseTransaction());
            UpdateCurrentExpense();
        });
    }

    public RelayCommand TodayCommand
    {
        get => new(() =>
        {
            UpdateChartData(_transactionsService.GetAllExepenseTransaction(), DateTime.Today.ToString("dd/MM/yyyy"));
        });
    }
}


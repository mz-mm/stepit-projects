using GalaSoft.MvvmLight.Messaging;
using Monefy.Services.Interfaces;
using Monefy.Services.Classes;
using Monefy.ViewModels;
using Monefy.Views;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Monefy;

public partial class App : Application
{
    public static Container Container { get; set; } = new();

    public void Register()
    {

        Container.RegisterSingleton<IMessenger , Messenger>();
        Container.RegisterSingleton<INavigationService, NavigationService>();

        Container.RegisterSingleton<IDeserializeService, DeserializeSerivce>();
        Container.RegisterSingleton<ISerializeService, SerializeService>();
        Container.RegisterSingleton<ITransactionsService, TransactionService>();
        
        Container.RegisterSingleton<TransactionsViewModel>();
        Container.RegisterSingleton<AddTransactionViewModel>();
        Container.RegisterSingleton<AnalyticsViewModel>();
        Container.RegisterSingleton<MainViewModel>();

        Container.Verify();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        Register();

        var window = new MainView
        {
            DataContext = Container.GetInstance<MainViewModel>()
        };

        window.ShowDialog();
    }
}

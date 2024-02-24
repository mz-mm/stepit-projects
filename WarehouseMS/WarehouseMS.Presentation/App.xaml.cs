using System;
using System.Windows;
using Microsoft.Extensions.Hosting;
using WarehouseMS.Domain.Services;
using WarehouseMS.Presentation.ViewModels;
using WarehouseMS.Presentation.Views;

namespace WarehouseMS.Presentation;

public partial class App
{
    public static IHost? AppHost { get; private set; }

    public App()
    {
        var startup = new Startup();
        AppHost = startup.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        ServiceLocator.Initialize(AppHost);

        var mainWindow = new MainView
        {
            DataContext = ServiceLocator.GetService<MainViewModel>()
        };

        mainWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }
}

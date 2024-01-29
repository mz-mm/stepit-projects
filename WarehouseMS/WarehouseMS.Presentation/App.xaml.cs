using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WarehouseMS.Infrastructure.Context;
using WarehouseMS.Presentation.ViewModels;
using WarehouseMS.Presentation.Views;

namespace WarehouseMS.Presentation;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDbContext<AppDbContext>(options =>
                {
                    // Configure your DbContext options here
                    options.UseSqlServer("YourConnectionString");
                });

                services.AddTransient<MainView>();
                services.AddTransient<MainViewModel>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var mainWindow = AppHost.Services.GetRequiredService<MainView>();
        mainWindow.DataContext = AppHost.Services.GetRequiredService<MainViewModel>();
        mainWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }
}

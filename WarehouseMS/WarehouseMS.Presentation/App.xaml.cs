using System;
using System.IO;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Domain.Services;
using WarehouseMS.Infrastructure.Context;
using WarehouseMS.Infrastructure.Interfaces;
using WarehouseMS.Infrastructure.Repositories;
using WarehouseMS.Presentation.Interfaces;
using WarehouseMS.Presentation.Services;
using WarehouseMS.Presentation.ViewModels;
using WarehouseMS.Presentation.Views;

namespace WarehouseMS.Presentation;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"));
                });

                services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

                services.AddTransient<IMessenger, Messenger>();
                services.AddTransient<INavigationService, NavigationService>();

                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IProductRepository, ProductRepository>();
                services.AddScoped<IOrderRepository, OrderRepository>();
                services.AddScoped<ICategoryRepository, CategoryRepository>();

                services.AddScoped<IAuthService, AuthService>();
                services.AddScoped<IUserService, UserService>();
                services.AddScoped<IProductService, ProductService>();
                services.AddScoped<IOrderService, OrderService>();
                services.AddScoped<ICategoryService, CategoryService>();

                services.AddTransient<MainView>();
                services.AddTransient<MainViewModel>();
                
                services.AddTransient<LoginView>();
                services.AddTransient<LoginViewModel>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        ServiceLocator.Initialize(AppHost);

        var mainWindow = ServiceLocator.GetService<MainView>();
        mainWindow.DataContext = ServiceLocator.GetService<MainViewModel>();
        mainWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }
}

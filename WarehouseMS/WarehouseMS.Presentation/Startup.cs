using System;
using System.IO;
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

public class Startup
{
    public IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseNpgsql(hostContext.Configuration.GetConnectionString("DefaultConnection"));
                });

                services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

                services.AddSingleton<IMessenger, Messenger>();
                services.AddSingleton<INavigationService, NavigationService>();

                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IProductRepository, ProductRepository>();
                services.AddScoped<IOrderRepository, OrderRepository>();
                services.AddScoped<IStatusViewRepository, StatusViewRepository>();
                services.AddScoped<IOrderProductRepository, OrderProductRepository>();
                services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();

                services.AddScoped<IAuthService, AuthService>();
                services.AddScoped<IUserService, UserService>();
                services.AddScoped<IProductService, ProductService>();
                services.AddScoped<IOrderService, OrderService>();
                services.AddScoped<IStatusViewService, StatusViewService>();
                services.AddScoped<IOrderStatusService, OrderStatusService>();

                services.AddTransient<MainView>();
                services.AddTransient<MainViewModel>();

                services.AddTransient<LoginView>();
                services.AddTransient<LoginViewModel>();

                services.AddTransient<SignupView>();
                services.AddTransient<SignupViewModel>();

                services.AddTransient<HomeView>();
                services.AddTransient<HomeViewModel>();

                services.AddTransient<ProductsView>();
                services.AddTransient<ProductsViewModel>();

                services.AddTransient<AddStatusView>();
                services.AddTransient<AddStatusViewModel>();

                services.AddTransient<AddProductView>();
                services.AddTransient<AddProductViewModel>();

                services.AddTransient<OrdersView>();
                services.AddTransient<OrdersViewModel>();

                services.AddTransient<AddOrderView>();
                services.AddTransient<AddOrderViewModel>();

                services.AddTransient<ManageUsersView>();
                services.AddTransient<ManageUsersViewModel>();

                services.AddTransient<ManageUsersView>();
                services.AddTransient<ManageUsersViewModel>();
            });
}
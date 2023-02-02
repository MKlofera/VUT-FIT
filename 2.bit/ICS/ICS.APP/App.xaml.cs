using ICS.App.Services;
using ICS.App.Settings;
using ICS.BL;
using ICS.DAL;
using ICS.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using ICS.App.ViewModel;
using ICS.App.ViewModel.Interfaces;
using ICS.App.Views;
using ICS.App.Extensions;

namespace ICS.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        public static MainWindow ParentWindowRef;

        public App()
        {

            Thread.CurrentThread.CurrentCulture = new CultureInfo("cs");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("cs");

            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureServices((context, services) => ConfigureServices(context.Configuration, services))
                .Build();
        }

        private static void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
        {
            builder.AddJsonFile(@"AppSettings.json", false, false);
        }

        private static void ConfigureServices(IConfiguration configuration,
            IServiceCollection services)
        {
            services.AddBLServices();

            services.Configure<DALSettings>(configuration.GetSection("ICS:DAL"));

            services.AddSingleton<IDbContextFactory<ICSDbContext>>(provider =>
            {
                var dalSettings = provider.GetRequiredService<IOptions<DALSettings>>().Value;
                return new SqlServerDbContextFactory(dalSettings.ConnectionString!, dalSettings.SkipMigrationAndSeedDemoData);
            });

            services.AddSingleton<MainWindow>();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<IMediator, Mediator>();
            services.AddFactory<IUserDetailViewModel, UserDetailViewModel>();
            services.AddFactory<IUserListViewModel, UserListViewModel>();
            services.AddSingleton<ICarListViewModel, CarListViewModel>();
            services.AddSingleton<ICarpoolListViewModel, CarpoolListViewModel>();
            services.AddSingleton<IRideListViewModel, RideListViewModel>();
            services.AddSingleton<IRideListBrowseViewModel, RideListBrowseViewModel>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var dbContextFactory = _host.Services.GetRequiredService<IDbContextFactory<ICSDbContext>>();

            var dalSettings = _host.Services.GetRequiredService<IOptions<DALSettings>>().Value;

            await using (var dbx = await dbContextFactory.CreateDbContextAsync())
            {
                if (dalSettings.SkipMigrationAndSeedDemoData)
                {
                    await dbx.Database.EnsureDeletedAsync();
                    await dbx.Database.EnsureCreatedAsync();
                }
                else
                {
                    await dbx.Database.MigrateAsync();
                }
            }

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }


    }
}

using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UPS.Application;

namespace Ups.App;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IConfiguration Config { get; private set; }
    public static IHost? AppHost { get; private set; }
    public App()
    {
        Config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<App>() 
            .Build();

    


        AppHost = Host.CreateDefaultBuilder().ConfigureServices(((context, services) =>
        {
            services.AddSingleton<MainWindow>();
            services.AddApplication(Config);

        })).Build();



    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var startUpForm = AppHost!.Services.GetRequiredService<MainWindow>();

        startUpForm.Show();
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }
}
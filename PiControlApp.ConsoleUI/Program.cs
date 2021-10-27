// Created: 2021|09|29
// Modified: 2021|10|13
// PiControlApp.ConsoleUI|Program.cs|PiControlApp
// Olaaf Rossi

using System;
using System.IO;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PiControlApp.ConsoleUI.DataAccess;
using PiControlApp.ConsoleUI.Devices;
using Refit;
using Serilog;

// scp -r C:\Dev\PiControlApp\PiControlApp.ConsoleUI\bin\Release\net5.0\publish\* pi@cm01:Desktop/Deployment/
// https://endjin.com/blog/2019/09/passwordless-ssh-from-windows-10-to-raspberry-pi
// scp id_ed25519.pub pi@cm01:~\.ssh\authorized_keys
// https://docs.microsoft.com/en-us/visualstudio/debugger/remote-debugging-dotnet-core-linux-with-ssh?view=vs-2019

namespace PiControlApp.ConsoleUI
{
    [UsedImplicitly]
    public class Program
    {
        private static IConfiguration _config;

        private static void Main(string[] args)
        {
            ConfigurationBuilder builder = new();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Application Starting");

            string weatherServerUrl = _config.GetValue<string>("WeatherServerUrl");

            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    //services.AddTransient<IMonitorService, MonitorService>();
                    services.AddSingleton<IWeatherSensor, WeatherSensor>();
                    services.AddSingleton<IGpioDevice, GpioDevice>();
                    services.AddRefitClient<IWeatherData>().ConfigureHttpClient(c => { c.BaseAddress = new Uri(weatherServerUrl); });
                })
                .UseSerilog()
                .Build();

            RunWeatherLoop(host);
        }

        private static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIORNMENT") ?? "Production"}.json", true)
                .AddEnvironmentVariables();

            IConfiguration config = builder.Build();
            _config = config;
        }

        private static void RunWeatherLoop(IHost host)
        {
            IMonitorService svc = ActivatorUtilities.CreateInstance<MonitorService>(host.Services);
            svc.RunLoop = true;
            svc.Run();

            if (svc.FailedSensorReadCount >= 1000)
            {
                svc.RunLoop = false;
            }
        }
    }
}
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;

namespace HotelListing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(path: "Z:\\PACKT\\ULTIMATE_ASP_NET_5_WEB_API\\HotelListing_LOG\\log-.txt"
                , outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                , rollingInterval: RollingInterval.Day
                , restrictedToMinimumLevel: LogEventLevel.Information).CreateLogger();

            try
            {
                Log.Information("Application Is Starting");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exc)
            {
                Log.Fatal(exc, "Application failed to start");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace IntroToDotNetCoreWebAPI
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build();

        private static LogEventLevel GetLogEventLevel()
        {
            LogEventLevel defaultLevel = LogEventLevel.Information;
            var logEventLevel = Environment.GetEnvironmentVariable("LOG_LEVEL");

            if (!string.IsNullOrEmpty(logEventLevel))
            {
                LogEventLevel level;
                if (!Enum.TryParse(logEventLevel, out level))
                {
                    Trace.TraceWarning("Error parsing Serilog.LogEventLevel. Defaulting to {0}", defaultLevel);
                    level = defaultLevel;
                }

                return level;
            }

            return defaultLevel;
        }

        private static string GetSerilogFileConfig()
        {
            return $"{Configuration["Serilog:TextFileDir"]}{Configuration["Serilog:LogName"]}.txt";
        }

        public static void Main(string[] args)
        {
            LogEventLevel minLevel = GetLogEventLevel();

            Trace.TraceInformation("Logging: minimal level is {0}.", minLevel);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(minLevel)
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Debug()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
                .WriteTo.File(GetSerilogFileConfig(), rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                Log.Information("Starting up");

                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    var context = services.GetRequiredService<AppDbContext>();
                    context.Database.Migrate();
                }

                host.Run();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
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

using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace cwdemo.infrastructure
{
    public static class StartupExtensions
    {
        public static void ConfigureLogger(this IServiceCollection services, bool isProduction, string appVersion = "")
        {
            var logger = new LoggerConfiguration();

            var logFormat = "{Timestamp:HH:mm:ss} [{Level:u3}] ({User}--{CorrelationToken}) {Message}{NewLine}{Exception}";

            Log.Logger = logger
                .MinimumLevel.Debug()
                .WriteTo.Console(isProduction ? LogEventLevel.Error : LogEventLevel.Debug, logFormat)
                //.WriteTo.File("Log/log.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: isProduction ? LogEventLevel.Information : LogEventLevel.Debug, outputTemplate: logFormat)
                // .WriteTo.Sentry("https://9e7f4bad7cc842c1bb78032e70a5ded0@sentry.io/1727790", restrictedToMinimumLevel: isProduction ? LogEventLevel.Error : LogEventLevel.Fatal, release: appVersion)
                .Enrich.FromLogContext()
                .CreateLogger();

            Log.Information("Application starting", null, null);
        }


    }
}

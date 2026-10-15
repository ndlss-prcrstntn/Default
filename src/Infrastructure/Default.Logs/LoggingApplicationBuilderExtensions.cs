using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Default.Logs;

public static class LoggingApplicationBuilderExtensions
{
    public static IHostApplicationBuilder BootstrapLogger(this IHostApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithThreadId()
            .WriteTo.Debug()
            .CreateBootstrapLogger();

        return builder;
    }
}
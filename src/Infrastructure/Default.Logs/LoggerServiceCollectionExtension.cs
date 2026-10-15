using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Default.DependencyInjection;
using Default.Hangfire;
using Default.Hosting;
using Default.Logs.Config;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Templates.Themes;
using SerilogTracing;
using SerilogTracing.Expressions;

namespace Default.Logs;

public static class LoggerServiceCollectionExtension
{
    public static IServiceCollection AddLoggerConfiguration(
        this IServiceCollection services, ConfigurationManager configuration)
    {
        services
            .ConfigureSection<LogConfigSection>(configuration)
            .AddSerilog((services, lc) =>
            {
                var env = services.GetRequiredService<IHostEnvironment>();

                var logConfig = services.GetRequiredService<IOptions<LogConfigSection>>().Value;
                lc.ReadFrom.Configuration(configuration)
                    .ReadFrom.Services(services)
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("System", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
                    .MinimumLevel.Override("Default.FileStorage", LogEventLevel.Error);

                // Containers (VPS) and non-local: Grafana gets errors only, not per-request Info noise.
                if (IsContainerOrNonLocal(env))
                    lc.MinimumLevel.Error();

                lc.Enrich.WithProperty("Environment", env.EnvironmentName)
                    .Enrich.WithMachineName()
                    .Enrich.WithThreadId()
                    .Enrich.FromLogContext()
                    .WriteTo.Debug()
                    // Pretty text only for interactive local runs. Containers (incl. VPS Debug)
                    // emit one JSON event per line so Loki/Grafana can expose keys as Fields.
                    .WriteTo.Console(UsePrettyConsole(env)
                        ? Formatters.CreateConsoleTextFormatter(TemplateTheme.Code)
                        : new RenderedCompactJsonFormatter())
                    .WriteTo.Sentry(s =>
                    {
                        s.Dsn = string.IsNullOrEmpty(logConfig.GlitchTip)
                            ? ""
                            : logConfig.GlitchTip;

                        s.MinimumBreadcrumbLevel = LogEventLevel.Debug;
                        s.MinimumEventLevel = env.IsDevelopment() ? LogEventLevel.Debug : LogEventLevel.Error;
                        s.Environment = env.EnvironmentName;
                    })
                    .Destructure.ToMaximumDepth(3)
                    .Destructure.With<TracedObjectDestructuringPolicy>();

                var configurators = services.GetServices<ISerilogSinkConfigurator>();

                foreach (var configurator in configurators)
                    configurator.Configure(lc, services);
            })
            .AddTransient<LoggingInterceptor>();

        using var listener = new ActivityListenerConfiguration()
            .TraceToSharedLogger();

        return services;
    }

    private static bool IsContainerOrNonLocal(IHostEnvironment env)
        => !env.IsLocal()
           || string.Equals(
               Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"),
               "true",
               StringComparison.OrdinalIgnoreCase);

    private static bool UsePrettyConsole(IHostEnvironment env)
        => env.IsLocal()
           && !string.Equals(
               Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"),
               "true",
               StringComparison.OrdinalIgnoreCase);
}
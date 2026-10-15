using System.Globalization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Default.Syntax.Reflection;
using Serilog.Debugging;

namespace Default.Hosting;

public static class HostApplicationBuilderExtensions
{
    public static IHostApplicationBuilder PrepareBuilder<T>(this IHostApplicationBuilder builder)
        where T : class
    {
        typeof(T).Assembly.SetAsRootAssembly();

        var culture = new CultureInfo("ru-RU");
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        SelfLog.Enable(Console.Error);

        var env = builder.Environment;

        builder.Configuration
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

        if (env.IsDebug() || env.IsDevelopment() || env.IsLocal())
        {
            builder.Configuration
                .AddJsonFile("appsettings.local.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.local.json", true, true);

            if (env.ApplicationName is { Length: > 0 })
                builder.Configuration.AddUserSecrets<T>();
        }

        builder.Configuration.AddEnvironmentVariables();


        return builder;
    }
}

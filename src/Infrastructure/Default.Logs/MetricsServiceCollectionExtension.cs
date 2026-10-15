using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;

namespace Default.Logs;

public static class MetricsServiceCollectionExtension
{
    public static IServiceCollection AddMetricsConfiguration(
        this IServiceCollection services)
    {
        services
            .AddOpenTelemetry()
            .WithMetrics(builder =>
            {
                builder
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddProcessInstrumentation()
                    .AddPrometheusExporter()
                    .AddMeter("Microsoft.AspNetCore.Hosting")
                    .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                    .AddView("http.server.request.duration",
                        new ExplicitBucketHistogramConfiguration
                        {
                            Boundaries =
                            [
                                0, 0.005, 0.01, 0.025, 0.05, 0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10, 600
                            ]
                        }
                    );
            });

        return services;
    }
}
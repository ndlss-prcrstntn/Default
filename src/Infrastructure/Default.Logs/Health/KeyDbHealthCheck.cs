using Microsoft.Extensions.Diagnostics.HealthChecks;
using StackExchange.Redis;

namespace Default.Logs.Health;

public sealed class KeyDbHealthCheck(string connectionString) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var options = ConfigurationOptions.Parse(connectionString);
            options.AbortOnConnectFail = true;
            options.ConnectTimeout = 2000;
            options.SyncTimeout = 2000;

            var multiplexer = await ConnectionMultiplexer.ConnectAsync(options);
            try
            {
                var pong = await multiplexer.GetDatabase().PingAsync();
                return HealthCheckResult.Healthy($"ping {pong.TotalMilliseconds:0}ms");
            }
            finally
            {
                await multiplexer.DisposeAsync();
            }
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("KeyDB is unavailable", ex);
        }
    }
}

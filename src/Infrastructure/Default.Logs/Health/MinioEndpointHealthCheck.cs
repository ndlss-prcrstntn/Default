using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Default.Logs.Health;

public sealed class MinioEndpointHealthCheck(HttpClient http, Uri healthUri) : IHealthCheck
{
    public const string HttpClientName = "minio-health";

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            using var response = await http.GetAsync(healthUri, cancellationToken);
            if (response.IsSuccessStatusCode)
                return HealthCheckResult.Healthy();

            return HealthCheckResult.Unhealthy($"MinIO health returned {(int)response.StatusCode}");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("MinIO is unavailable", ex);
        }
    }
}

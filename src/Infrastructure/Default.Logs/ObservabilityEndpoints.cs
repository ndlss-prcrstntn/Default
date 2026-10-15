namespace Default.Logs;

public static class ObservabilityEndpoints
{
    public const string Live = "/health/live";
    public const string Ready = "/health/ready";
    public const string Metrics = "/metrics";
    public const string Version = "/version";
}

public static class ObservabilityHealthTags
{
    public const string Live = "live";
    public const string Ready = "ready";
}

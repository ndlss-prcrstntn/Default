using Microsoft.Extensions.Hosting;

namespace Default.Hosting;

public static class EnvironmentExtensions
{
    public static bool IsDebug(this IHostEnvironment env)
        => env.EnvironmentName == "Debug";

    public static bool IsLocal(this IHostEnvironment env)
        => env.EnvironmentName.StartsWith("Debug");

}
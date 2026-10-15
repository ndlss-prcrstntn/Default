using Default.DependencyInjection;
using Default.Web;

namespace Default.Api.DependencyInjection;

public static class CorsServiceCollectionExtension
{
    public static IServiceCollection AddFrontendCors(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureSection<CorsConfigSection>(configuration);

        var section = configuration.GetSection(CorsConfigSection.Name);
        var origins = CorsOriginAllowlist.Resolve(
            section["origins"],
            section.GetSection("origins").Get<string[]>() ?? []);

        if (origins.Length == 0)
            throw new InvalidOperationException(
                "Configuration cors:origins must list at least one frontend origin.");

        services.AddCors(options =>
        {
            options.AddPolicy(CorsOriginAllowlist.PolicyName, policy =>
            {
                policy.SetIsOriginAllowed(origin => CorsOriginAllowlist.IsAllowed(origins, origin))
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        return services;
    }
}

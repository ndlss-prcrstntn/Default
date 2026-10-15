using System.Text;
using Default.DependencyInjection;
using Default.Hosting;
using Default.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Default.Api.DependencyInjection;

public static class JwtBearerServiceCollectionExtension
{
    public static IServiceCollection AddJwtBearerConfiguration(
        this IServiceCollection services, ConfigurationManager configuration, IHostEnvironment env)
    {
        services
            .ConfigureSection<JwtConfigSection>(configuration)
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = !env.IsLocal();
                options.SaveToken = true;
                options.TokenValidationParameters = GetParameters(configuration);

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        string accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;

                        if (!string.IsNullOrEmpty(accessToken) && path.Value!.Contains("hub", StringComparison.OrdinalIgnoreCase))
                        {
                            context.Token = accessToken;
                            return Task.CompletedTask;
                        }

                        if (context.Request.Cookies.TryGetValue("accessToken", out var cookieToken))
                            context.Token = cookieToken;

                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }

    private static TokenValidationParameters GetParameters(IConfiguration configuration)
    {
        var jwtTokenSettings = configuration.GetSection(JwtConfigSection.Name).Get<JwtConfigSection>()
            ?? throw new InvalidOperationException("jwt configuration section is required.");

        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = jwtTokenSettings.Issuer,
            ValidAudience = jwtTokenSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtTokenSettings.Secret)),
            ClockSkew = TimeSpan.Zero
        };
    }
}

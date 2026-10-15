using Default.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Default.Mail;

public static class EmailServiceCollectionExtensions
{
    public static IServiceCollection AddEmail(this IServiceCollection serviceCollection,
        IConfiguration configuration) =>
        serviceCollection
            .ConfigureSection<EmailConfigSection>(configuration)
            .AddScoped<IEmailService, EmailService>();
}
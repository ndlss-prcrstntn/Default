using System.Reflection;
using Default.DataAccess.Config;
using Default.DataAccess.EntityFramework;
using Default.DataAccess.EntityFramework.Abstractions;
using Default.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Default.DataAccess;

public static class DataAccessServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess<TDbContext>(
        this IServiceCollection serviceCollection,
        IConfiguration configuration, Assembly assembly = null)
        where TDbContext : ServiceDbContext
        => serviceCollection
            .ConfigureSection<DataAccessConfigSection>(configuration)
            .AddDbContext<ServiceDbContext, TDbContext>((sp, ob) =>
            {
                var config = sp.GetRequiredService<IOptions<DataAccessConfigSection>>().Value;
                ob.UseNpgsql(config.Connection,
                        npgOpt =>
                        {
                            npgOpt.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                            // Domain tables: schema tba. History must stay in public (see docs/deploy-vps.md).
                            npgOpt.MigrationsHistoryTable("__EFMigrationsHistory", "public");
                            if (assembly != null)
                                npgOpt.MigrationsAssembly(assembly.GetName().Name);
                        })
                    .UseLazyLoadingProxies();

                ob.AddInterceptors(
                    serviceCollection
                        .BuildServiceProvider()
                        .GetServices<ISaveChangesInterceptor>()
                        .ToArray<IInterceptor>());
            })
            .AddScoped<IUnitOfWork, UnitOfWork>();
}
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Default.Syntax;

namespace Default.DependencyInjection;

public static class InterceptorServiceCollectionExtensions
{
    internal static IServiceCollection AddInterceptorsForType(this IServiceCollection serviceCollection,
        Type[] interceptorTypes, Type implementation)
    {
        interceptorTypes.ForEach(serviceCollection.TryAddTransient);

        return serviceCollection;
    }

    private static IServiceCollection AddWithInterceptors(
        this IServiceCollection serviceCollection,
        Type serviceType,
        Type implementationType,
        Type[] interceptorTypes,
        ServiceLifetime serviceLifetime)
    {
        serviceCollection.TryAddSingleton<ProxyGenerator>();
        serviceCollection.Add(new ServiceDescriptor(implementationType, implementationType, serviceLifetime));

        serviceCollection
            .Add(new ServiceDescriptor(serviceType, sp =>
            {
                var implementation = sp.GetRequiredService(implementationType);
                var interceptors = interceptorTypes.Select(sp.GetRequiredService).Cast<IInterceptor>().ToArray();
                var interfaceProxyWithTarget = sp.GetRequiredService<ProxyGenerator>()
                    .CreateInterfaceProxyWithTarget(serviceType, implementation, interceptors);

                return interfaceProxyWithTarget;
            }, serviceLifetime));

        return serviceCollection.AddInterceptorsForType(interceptorTypes, implementationType);
    }

    public static IServiceCollection AddScopedWithInterceptors<TService, TImplementation, TInterceptor>(
        this IServiceCollection serviceCollection)
        where TService : class
        where TImplementation : class, TService
        where TInterceptor : class, IInterceptor =>
        serviceCollection.AddWithInterceptors(
            typeof(TService),
            typeof(TImplementation),
            typeof(TInterceptor).AsArray(),
            ServiceLifetime.Scoped);

    public static IServiceCollection AddTransientWithInterceptors<TService, TImplementation, TInterceptor>(
        this IServiceCollection serviceCollection)
        where TService : class
        where TImplementation : class, TService
        where TInterceptor : class, IInterceptor =>
        serviceCollection.AddWithInterceptors(
            typeof(TService),
            typeof(TImplementation),
            typeof(TInterceptor).AsArray(),
            ServiceLifetime.Transient);

    public static IServiceCollection AddTransientWithInterceptors(this IServiceCollection serviceCollection,
        Type serviceType,
        Type implementationType,
        Type[] interceptorTypes) =>
        serviceCollection.AddWithInterceptors(
            serviceType,
            implementationType,
            interceptorTypes,
            ServiceLifetime.Transient);

    public static IServiceCollection AddSingletonWithInterceptors<TService, TImplementation, TInterceptor>(
        this IServiceCollection serviceCollection)
        where TService : class
        where TImplementation : class, TService
        where TInterceptor : class, IInterceptor =>
        serviceCollection.AddWithInterceptors(
            typeof(TService),
            typeof(TImplementation),
            typeof(TInterceptor).AsArray(),
            ServiceLifetime.Singleton);

    public static IServiceCollection AddSingletonWithInterceptors(this IServiceCollection serviceCollection,
        Type serviceType,
        Type implementationType,
        Type[] interceptorTypes) =>
        serviceCollection.AddWithInterceptors(
            serviceType,
            implementationType,
            interceptorTypes,
            ServiceLifetime.Singleton);
}
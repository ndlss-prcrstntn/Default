using Amazon.Runtime;
using Amazon.S3;
using Default.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Default.FileStorage;

public static class S3ServiceCollectionExtensions
{
    public static IServiceCollection AddFileStorage(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        return serviceCollection
            .ConfigureSection<S3ConfigSection>(configuration)
            .AddSingleton<IAmazonS3>(sp =>
            {
                var cfg = sp.GetRequiredService<IOptions<S3ConfigSection>>().Value;

                var s3cfg = new AmazonS3Config
                {
                    ServiceURL = cfg.Endpoint,
                    ForcePathStyle = true,
                    AuthenticationRegion = cfg.Region
                };

                return new AmazonS3Client(
                    new BasicAWSCredentials(cfg.AccessKey, cfg.AccessKeySecret),
                    s3cfg
                );
            })
            .AddScoped<IFileStorageService, FileStorageService>();
    }
}
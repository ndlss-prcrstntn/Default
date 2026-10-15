using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Default.Cache;

public class KeyDbConnectionFactory(
    IOptions<KeyDbConfigSection> options,
    ILogger<CacheStorage> logger)
{
    private readonly KeyDbConfigSection _configSection = options.Value;

    public ConnectionMultiplexer CreateMultiplexer()
    {
        try
        {
            var connectionMultiplexer = ConnectionMultiplexer.Connect(_configSection.Connection);
            connectionMultiplexer.ConnectionFailed += (o, e) =>
            {
                logger.LogError(e.Exception, "KeyDB connection to {EndPoint} failed with {FailureType}.",
                    e.EndPoint, e.FailureType);
            };
            connectionMultiplexer.ConnectionRestored += (o, e) =>
            {
                logger.LogWarning("KeyDB connection to {EndPoint} restored", e.EndPoint);
            };
            connectionMultiplexer.ErrorMessage += (o, e) =>
            {
                logger.LogError("KeyDB connection to {EndPoint} receive error message {ErrorMessage}.",
                    e.EndPoint, e.Message);
            };

            return connectionMultiplexer;
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "KeyDB connection error");
            throw;
        }
    }
}
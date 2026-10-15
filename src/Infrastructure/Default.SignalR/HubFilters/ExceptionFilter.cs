using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Default.SignalR.HubFilters;

public class ExceptionFilter(ILogger<ExceptionFilter> logger) : IHubFilter
{
    public async ValueTask<object?> InvokeMethodAsync(
        HubInvocationContext invocationContext,
        Func<HubInvocationContext, ValueTask<object?>> next)
    {
        try
        {
            return await next(invocationContext);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "SignalR {Hub}.{Method} failed for {ConnectionId}",
                invocationContext.Hub.GetType().Name,
                invocationContext.HubMethodName,
                invocationContext.Context.ConnectionId);
            throw;
        }
    }
}

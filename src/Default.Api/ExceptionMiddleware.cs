using System.Net;
using Default.Abstractions.Exceptions;

namespace Default.Api;

public class ExceptionMiddleware(
    RequestDelegate next,
    ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (OperationCanceledException)
        {
            logger.LogWarning("Request {Path} was cancelled", context.Request.Path);
        }
        catch (PermissionDeniedException ex)
        {
            logger.LogWarning(ex, "PermissionDeniedException");
            await WriteResponseSafeAsync(context, HttpStatusCode.Forbidden, ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "KeyNotFoundException");
            await WriteResponseSafeAsync(context, HttpStatusCode.NotFound, ex.Message);
        }
        catch (ClientException ex)
        {
            logger.LogWarning(ex, "ClientException");
            await WriteResponseSafeAsync(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");
            await WriteResponseSafeAsync(
                context,
                HttpStatusCode.InternalServerError,
                "Request could not be processed. Try again.");
        }
    }

    private static async Task WriteResponseSafeAsync(
        HttpContext context,
        HttpStatusCode status,
        string message)
    {
        try
        {
            if (context.Response.HasStarted)
                return;

            context.Response.ContentType = "text/plain; charset=utf-8";
            context.Response.StatusCode = (int)status;
            await context.Response.WriteAsync(message);
        }
        catch (InvalidOperationException)
        {
        }
    }
}

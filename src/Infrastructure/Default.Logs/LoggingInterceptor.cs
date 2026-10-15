using System.Diagnostics;
using System.Reflection;
using Castle.DynamicProxy;
using Serilog;

namespace Default.Logs;

public class LoggingInterceptor(ILogger logger) : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        var parameters = invocation.Method.GetParameters();
        var args = new Dictionary<string, object?>();

        for (var i = 0; i < parameters.Length; i++)
        {
            var param = parameters[i];

            if (param.ParameterType == typeof(CancellationToken))
                continue;

            var name = param.Name ?? $"arg{i}";
            var value = invocation.Arguments[i];

            if (param.GetCustomAttribute<SensitiveAttribute>() != null)
                args[name] = "***MASKED***";
            else
                args[name] = value;
        }

        logger.Information("Вызов метода {Method} с аргументами {@Args}",
            invocation.Method.Name, args);

        var sw = Stopwatch.StartNew();

        try
        {
            invocation.Proceed();

            if (IsTask(invocation.Method.ReturnType))
            {
                var task = (Task)invocation.ReturnValue;
                task.ContinueWith(t =>
                {
                    sw.Stop();

                    if (t.IsFaulted)
                    {
                        var ex = t.Exception?.GetBaseException() ?? t.Exception;
                        logger.Error(ex,
                            "Ошибка в методе {Method}. Время до ошибки: {Milliseconds} ms.",
                            invocation.Method.Name,
                            sw.Elapsed.TotalMilliseconds);
                    }
                    else
                    {
                        logger.Information(
                            "Метод {Method} успешно выполнен за {Milliseconds} ms.",
                            invocation.Method.Name,
                            sw.Elapsed.TotalMilliseconds);
                    }
                });
            }
            else
            {
                sw.Stop();
                logger.Information(
                    "Метод {Method} вернул {@Result} за {Milliseconds} ms.",
                    invocation.Method.Name,
                    invocation.ReturnValue,
                    sw.Elapsed.TotalMilliseconds);
            }
        }
        catch (Exception ex)
        {
            sw.Stop();
            logger.Error(ex,
                "Ошибка в методе {Method}. Время до ошибки: {Milliseconds} ms.",
                invocation.Method.Name,
                sw.Elapsed.TotalMilliseconds);
            throw;
        }
    }

    private static bool IsTask(Type type) =>
        type == typeof(Task) ||
        (type.IsGenericType &&
         type.GetGenericTypeDefinition() == typeof(Task<>));
}
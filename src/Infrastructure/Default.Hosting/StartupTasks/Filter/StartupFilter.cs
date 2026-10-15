using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Default.Hosting.StartupTasks.Filter;

public class StartupFilter : IStartupFilter
{
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return app =>
        {
            var services = app.ApplicationServices.GetServices<IStartupTask>();
            foreach (var startupTask in services.OrderBy(s => s.Order()))
                startupTask.ExecuteAsync().GetAwaiter().GetResult();

            next(app);
        };
    }
}
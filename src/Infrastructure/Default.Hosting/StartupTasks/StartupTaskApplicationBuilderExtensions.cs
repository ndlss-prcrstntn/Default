using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Default.Hosting.StartupTasks;

public static class StartupTaskApplicationBuilderExtensions
{
    public static async Task UseStartupTasksAsync(this IApplicationBuilder app)
    {
        var services = app.ApplicationServices.GetServices<IStartupTask>();
        foreach (var startupTask in services.OrderBy(s => s.Order())) await startupTask.ExecuteAsync();
    }
}
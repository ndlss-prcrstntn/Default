using Default.Api;
using Default.Api.DependencyInjection;
using Default.DataAccess;
using Default.DataAccess.EntityFramework;
using Default.Hosting;
using Default.Hosting.StartupTasks;
using Default.Logs;
using Default.Scheduler.DependencyInjection;
using Default.SignalR;
using Default.Web;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.BootstrapLogger().PrepareBuilder<Program>();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor
                               | ForwardedHeaders.XForwardedProto
                               | ForwardedHeaders.XForwardedHost;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

builder.Services
    .AddLoggerConfiguration(configuration)
    .AddMetricsConfiguration()
    .AddObservabilityHealthChecks(configuration)
    .AddDataAccess<ApplicationDbContext>(configuration, typeof(Program).Assembly)
    .AddAppServices(configuration)
    .AddStartupTasks()
    .AddJwtBearerConfiguration(configuration, builder.Environment)
    .AddSwaggerDocumentation()
    .AddFrontendCors(configuration)
    .AddScheduler(configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseForwardedHeaders();
app.Migrate<ApplicationDbContext>(args);

if (app.Environment.IsLocal())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsLocal())
{
    app.UseHttpsRedirection();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(CorsOriginAllowlist.PolicyName);
app.UseAuthentication();
app.UseAuthorization();

app.MapObservability();
app.MapControllers();
app.MapDefaultSignalR();

app.Run();

public partial class Program;

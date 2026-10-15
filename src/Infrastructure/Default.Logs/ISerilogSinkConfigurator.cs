using Serilog;

namespace Default.Hangfire;

public interface ISerilogSinkConfigurator
{
    void Configure(LoggerConfiguration loggerConfiguration, IServiceProvider sp);
}
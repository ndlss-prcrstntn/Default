using System.Reflection;
using Default.Abstractions;
using Default.Syntax.Reflection;

namespace Default.Hosting;

public class AssemblyVersionExtractor : IVersionExtractor
{
    private readonly Assembly _assembly =
        AssemblyRoot.GetRootAssembly()
        ?? Assembly.GetEntryAssembly()
        ?? typeof(AssemblyVersionExtractor).Assembly;

    public string GetVersion()
    {
        var informational = _assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion;

        if (!string.IsNullOrWhiteSpace(informational))
        {
            // Strip optional "+metadata" / git describe suffixes.
            var plus = informational.IndexOf('+');
            return plus >= 0 ? informational[..plus] : informational;
        }

        return _assembly.GetName().Version?.ToString() ?? "0.0.0";
    }

    public string GetCompanyName() => _assembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company;

    public string GetCopyright() => _assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;
}

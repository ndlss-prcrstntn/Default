using Default.Abstractions;

namespace Default.Logs.Config;

public class LogConfigSection : IConfigSection
{
    public static readonly string Name = "log";

    public string? GlitchTip { get; set; }
}
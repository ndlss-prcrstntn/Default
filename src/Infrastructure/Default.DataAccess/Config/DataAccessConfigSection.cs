using Default.Abstractions;

namespace Default.DataAccess.Config;

public class DataAccessConfigSection : IConfigSection
{
    public static readonly string Name = "dataAccess";

    public string Connection { get; set; }

    public string Schema { get; set; } = null;
}
using Default.Abstractions;

namespace Default.Ext.License;

public class LicenseConfigSection : IConfigSection
{
    public static readonly string Name = "license";

    public string Key { get; set; }
}
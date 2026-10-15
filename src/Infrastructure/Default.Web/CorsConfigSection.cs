using Default.Abstractions;

namespace Default.Web;

public class CorsConfigSection : IConfigSection
{
    public static readonly string Name = "cors";

    public string[] Origins { get; set; } = [];
}

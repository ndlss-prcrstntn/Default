namespace Default.Abstractions;

public interface IVersionExtractor
{
    string GetVersion();

    string GetCompanyName();

    string GetCopyright();
}
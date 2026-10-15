namespace Default.DataAccess.EntityFramework.Abstractions;

public class AudiableProperty(string title, Func<object, string> funcToReadProperty)
{
    public string Title { get; init; } = title;

    public Func<object, string> GetProperty { get; init; } = funcToReadProperty;
}
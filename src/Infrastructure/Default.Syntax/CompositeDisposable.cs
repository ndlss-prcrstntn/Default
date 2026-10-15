namespace Default.Syntax;

public class CompositeDisposable(params IDisposable[] disposables) : IDisposable
{
    public void Dispose()
    {
        foreach (var d in disposables) d.Dispose();
    }
}
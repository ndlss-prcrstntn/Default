using System.Reflection;

namespace Default.Syntax.Reflection;

public static class AssemblyRoot
{
    private static Assembly? _rootAssembly = Assembly.GetEntryAssembly();

    public static void SetAsRootAssembly(this Assembly assembly)
    {
        _rootAssembly = assembly;
    }

    public static Assembly? GetRootAssembly() => _rootAssembly;
}
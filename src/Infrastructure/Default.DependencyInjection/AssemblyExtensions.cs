using Default.Syntax;
using System.Reflection;
using TypeFilter = Default.Syntax.TypeFilter;

namespace Default.DependencyInjection;

public static class AssemblyExtensions
{
    public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly, TypeFilter? typeFilter = null)
    {
        try
        {
            var index = assembly?.FullName?.IndexOfAny(['.', ',']);

            return index is null or < 0
                ? Array.Empty<Type>()
                : assembly?.GetTypes()
                    .Where(t => !t.GetTypeInfo().IsAbstract)
                    .Where(t => t.GetTypeInfo().IsClass)
                    .Where(t => typeFilter?.FilterExpression?.Invoke(t) ?? true);
        }
        catch (ReflectionTypeLoadException e)
        {
            Console.WriteLine(e);
            var types = e.Types;
            return types.Where(t => t != null);
        }
    }

    public static IEnumerable<Type> GetLoadableTypesWithReferenced(this Assembly assembly, TypeFilter typeFilter = null)
    {
        var assemblyCache = new Dictionary<string, Assembly>();

        void CheckAssembly(Assembly referencedAssembly)
        {
            assemblyCache[referencedAssembly.FullName] = referencedAssembly;

            var assemblyNames = referencedAssembly.GetReferencedAssemblies();

            assemblyNames.Where(an => !assemblyCache.ContainsKey(an.FullName))
                .Select(Assembly.Load).ForEach(CheckAssembly);
        }

        CheckAssembly(assembly);

        return assemblyCache.Values.SelectMany(a => a.GetLoadableTypes(typeFilter));
    }
}
namespace Default.Syntax;

public class TypeFilter
{
    private TypeFilter(Func<Type, bool> filter)
    {
        FilterExpression = filter;
    }

    public Func<Type, bool> FilterExpression { get; }

    public static TypeFilter AssignableTo<T>() => AssignableTo(typeof(T));

    public static TypeFilter AssignableTo(Type type) =>
        new(t => type.IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false });
}
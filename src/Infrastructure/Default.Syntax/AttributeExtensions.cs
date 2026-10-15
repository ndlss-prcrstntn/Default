using System.Reflection;

namespace Default.Syntax;

public static class AttributeExtensions
{
    public static T? GetAttribute<T>(this MemberInfo member) where T : Attribute =>
        member.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
}
using Default.Api.Core.Attributes;

namespace Default.Api.Core.Extensions;

public static class EnumExtension
{
    public static string StringValue(this Enum @enum)
    {
        var type = @enum.GetType();
        var fieldInfo = type.GetField(@enum.ToString());
        var attributes = fieldInfo.GetCustomAttributes(
            typeof(StringValueAttribute), false) as StringValueAttribute[];

        return attributes?.FirstOrDefault()?.StringValue;
    }
}
namespace Default.Api.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class StringValueAttribute(string stringValue) : Attribute
    {
        public string StringValue { get; } = stringValue;
    }
}

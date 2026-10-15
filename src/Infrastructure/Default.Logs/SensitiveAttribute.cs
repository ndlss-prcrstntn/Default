namespace Default.Logs;

[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
public sealed class SensitiveAttribute : Attribute
{
}
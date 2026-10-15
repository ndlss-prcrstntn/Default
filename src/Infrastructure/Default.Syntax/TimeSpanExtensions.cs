using Default.Syntax.Enum;

namespace Default.Syntax;

public static class TimeSpanExtensions
{
    public static TimeIndicator GetTimeIndicator(this TimeSpan timeSpan, TimeSpan threshold, bool fromStartDirection)
    {
        var diff = fromStartDirection
            ? timeSpan.TotalMinutes - threshold.TotalMinutes
            : threshold.TotalMinutes - timeSpan.TotalMinutes;
        return diff switch
        {
            > 15 => TimeIndicator.Danger,
            <= 5 => TimeIndicator.Success,
            _ => TimeIndicator.Warning
        };
    }
}
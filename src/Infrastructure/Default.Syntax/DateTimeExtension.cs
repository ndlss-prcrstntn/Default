using Default.Syntax.Enum;

namespace Default.Syntax;

public static class DateTimeExtension
{
    public static TimeIndicator GetDateTimeIndicator(
        this DateTime dateTime, double hours, bool fromStartDirection)
    {
        var dateTimeToCompare = dateTime.Date.AddHours(hours);

        var totalMinutesDiff = fromStartDirection
            ? (dateTime - dateTimeToCompare).TotalMinutes
            : (dateTimeToCompare - dateTime).TotalMinutes;

        return totalMinutesDiff switch
        {
            > 15 => TimeIndicator.Danger,
            <= 5 => TimeIndicator.Success,
            _ => TimeIndicator.Warning
        };
    }

    public static bool IsWeekend(this DateTime dateTime) =>
        dateTime.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

    public static bool IsWeekend(this DateTimeOffset dateTime) =>
        dateTime.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;


    public static int GetWorkDaysCount(this DateTimeOffset start, DateTimeOffset end)
    {
        if (start > end)
            (start, end) = (end, start);

        var count = 0;

        for (var date = start.Date; date < end.Date; date = date.AddDays(1))
            if (date.DayOfWeek != DayOfWeek.Saturday &&
                date.DayOfWeek != DayOfWeek.Sunday)
                count++;

        return count;
    }
}
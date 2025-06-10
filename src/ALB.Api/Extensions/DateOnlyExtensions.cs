namespace ALB.Api.Extensions;

public static class DateOnlyExtensions
{
    public static DateTime ToDateTime(this DateOnly date, int hour = 0, int minute = 0, int second = 0)
    {
        return new DateTime(date.Year, date.Month, date.Day, hour, minute, second);
    }
}
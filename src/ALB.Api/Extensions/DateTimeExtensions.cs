using NodaTime;

namespace ALB.Api.Extensions;

public static class DateTimeExtensions
{
    public static LocalTime ToNodaLocalTime(this DateTime dateTime)
        => LocalTime.FromTimeOnly(TimeOnly.FromDateTime(dateTime));
    
    public static LocalTime? ToNodaLocalTime(this DateTime? dateTime)
        => dateTime.HasValue ? LocalTime.FromTimeOnly(TimeOnly.FromDateTime(dateTime.Value)) : null;
}
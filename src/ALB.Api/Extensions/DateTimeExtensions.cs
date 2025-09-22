using System.Text.Json;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

namespace ALB.Api.Extensions;

public static class DateTimeExtensions
{
    public static LocalTime ToNodaLocalTime(this DateTime dateTime)
        => LocalTime.FromTimeOnly(TimeOnly.FromDateTime(dateTime));
    
    public static LocalTime? ToNodaLocalTime(this DateTime? dateTime)
        => dateTime.HasValue ? LocalTime.FromTimeOnly(TimeOnly.FromDateTime(dateTime.Value)) : null;
    
    public static DateOnly ToDateOnly(this LocalDate d) => new(d.Year, d.Month, d.Day);
    public static LocalDate ToLocalDate(this DateOnly d) => new(d.Year, d.Month, d.Day);
    
    public static LocalTime ToLocalTime(this TimeOnly t)
        => LocalTime.FromTicksSinceMidnight(t.Ticks);

    public static LocalTime? ToLocalTime(this TimeOnly? t)
        => t.HasValue ? LocalTime.FromTicksSinceMidnight(t.Value.Ticks) : (LocalTime?)null;

    public static TimeOnly ToTimeOnly(this LocalTime t)
        => TimeOnly.FromTimeSpan(TimeSpan.FromTicks(t.TickOfDay));

    public static TimeOnly? ToTimeOnly(this LocalTime? t)
        => t.HasValue ? TimeOnly.FromTimeSpan(TimeSpan.FromTicks(t.Value.TickOfDay)) : (TimeOnly?)null;

    public static IServiceCollection AddNodaTimeJsonConverters(this IServiceCollection serviceCollection)
    {
        serviceCollection.Configure<JsonSerializerOptions>(opts =>
        {
            opts.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
            opts.Converters.Add(NodaConverters.IntervalConverter);
            opts.Converters.Add(NodaConverters.InstantConverter);
            opts.Converters.Add(NodaConverters.LocalDateConverter);
            opts.Converters.Add(NodaConverters.LocalDateTimeConverter);
            opts.Converters.Add(NodaConverters.LocalTimeConverter);
        });
        
        return serviceCollection;
    }
}
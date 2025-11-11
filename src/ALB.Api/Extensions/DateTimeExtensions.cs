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
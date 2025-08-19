using System.Text.Json;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

namespace ApiIntegrationTests.Serialisation;

public class NodaTimeJsonSerialisationTests
{
    [Test]
    private async Task Should_Serialize_LocalDate_Correctly()
    {
        var explicitOptions = new JsonSerializerOptions
        {
            Converters = { NodaConverters.IntervalConverter, 
                NodaConverters.InstantConverter,
                NodaConverters.LocalDateConverter,
                NodaConverters.LocalDateTimeConverter, 
                NodaConverters.LocalTimeConverter }
        };
        
        var localDate = new LocalDate(2023, 10, 1);
        var json = JsonSerializer.Serialize(localDate, explicitOptions);

        var deserializedLocalDate = JsonSerializer.Deserialize<LocalDate>(json);

        await Assert.That(deserializedLocalDate).IsEqualTo(localDate);
    }
}
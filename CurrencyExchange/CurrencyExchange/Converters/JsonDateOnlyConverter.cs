using System.Text.Json;
using System.Globalization;
using System.Text.Json.Serialization;

namespace CurrencyExchange.Converters;

// ISO 8601 style date format
// TODO: Remove this class when DateOnly conversion is supported
// Issue: https://github.com/dotnet/runtime/issues/53539
public class JsonDateOnlyConverter : JsonConverter<DateOnly>
{
    private const string Format = "yyyy-MM-dd";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => DateOnly.ParseExact(reader.GetString()!, Format, CultureInfo.InvariantCulture);

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
}
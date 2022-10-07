using System.Text.Json;

using CurrencyExchange.Converters;

namespace CurrencyExchange.Extensions;

public static class DateOnlyConverterExtensions
{
    public static void AddDateOnlyConverters(this JsonSerializerOptions options)
    {
        options.Converters.Add(new JsonDateOnlyConverter());
    }
}
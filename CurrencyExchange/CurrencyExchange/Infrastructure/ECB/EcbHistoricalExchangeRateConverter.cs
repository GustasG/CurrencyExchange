using System.Xml;
using System.Collections;
using System.Globalization;

using CurrencyExchange.Domain;

namespace CurrencyExchange.Infrastructure.ECB;

public class EcbHistoricalExchangeRateConverter : IHistoricalExchangeRateConverter
{
    private static readonly string BaseCurrency = "EUR";
    
    private IDictionary<DateOnly, IDictionary<string, decimal>> _rates;

    public EcbHistoricalExchangeRateConverter()
    {
        _rates = FetchRates();
    }
    
    public IEnumerable<DateOnly> AvailableDates()
    {
        return _rates.Keys;
    }

    public IEnumerable<string> AvailableCurrencies(DateOnly date)
    {
        var rates = _rates[date];
        var currencies = new List<string>();
        currencies.Add(BaseCurrency);
        currencies.AddRange(rates.Keys);

        return currencies;
    }

    public decimal ExchangeRate(DateOnly date, string @from, string to)
    {
        var rates = _rates[date];
        
        if (from == to)
        {
            return 1.0m;
        }
        if (from == BaseCurrency)
        {
            return rates[to];
        }
        if (to == BaseCurrency)
        {
            return 1.0m / rates[from];
        }

        return rates[to] / rates[from];
    }

    public void Update()
    {
        _rates = FetchRates();
    }
    
    private static IDictionary<DateOnly, IDictionary<string, decimal>> FetchRates()
    {
        var document = new XmlDocument();
        document.Load("https://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist.xml?ff2570d11119724a6a0bcbca6cb04990");

        var manager = new XmlNamespaceManager(document.NameTable);
        manager.AddNamespace("gesmes", "http://www.gesmes.org/xml/2002-08-01");
        manager.AddNamespace("ecb", "http://www.ecb.int/vocabulary/2002-08-01/eurofxref");

        return document
            .SelectNodes("/gesmes:Envelope/ecb:Cube/ecb:Cube", manager)!
            .Cast<XmlNode>()
            .ToDictionary(node => DateOnly.ParseExact(node.Attributes!["time"]!.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                ExtractRates);
    }

    private static IDictionary<string, decimal> ExtractRates(IEnumerable data)
    {
        return data
            .Cast<XmlNode>()
            .ToDictionary(node => node.Attributes!["currency"]!.Value,
                node => decimal.Parse(node.Attributes!["rate"]!.Value, CultureInfo.InvariantCulture));
    }
}
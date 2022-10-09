using System.Xml;
using System.Globalization;

using CurrencyExchange.Domain;

namespace CurrencyExchange.Infrastructure.ECB;

public class EcbExchangeRateConverter : IExchangeRateConverter
{
    private static readonly string BaseCurrency = "EUR";
    
    private IDictionary<string, decimal> _rates;

    public EcbExchangeRateConverter()
    {
        _rates = FetchRates();
    }
    
    public IEnumerable<string> AvailableCurrencies()
    {
        var currencies = new List<string>();
        currencies.Add(BaseCurrency);
        currencies.AddRange(_rates.Keys);

        return currencies;
    }

    public decimal ExchangeRate(string from, string to)
    {
        if (from == to)
        {
            return 1.0m;
        }
        if (from == BaseCurrency)
        {
            return _rates[to];
        }
        if (to == BaseCurrency)
        {
            return 1.0m / _rates[from];
        }

        return _rates[to] / _rates[from];
    }

    public void Update()
    {
        _rates = FetchRates();
    }

    private static IDictionary<string, decimal> FetchRates()
    {
        var document = new XmlDocument();
        document.Load("https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");

       return document
           .SelectNodes("/*/*/*/*")!
           .Cast<XmlNode>()
           .ToDictionary(node => node.Attributes!["currency"]!.Value, 
               node => decimal.Parse(node.Attributes!["rate"]!.Value, CultureInfo.InvariantCulture));
    }
}
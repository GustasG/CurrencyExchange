namespace CurrencyExchange.Domain;

public interface IExchangeRateConverter
{
    IEnumerable<string> AvailableCurrencies();

    decimal ExchangeRate(string from, string to);

    void Update();
}
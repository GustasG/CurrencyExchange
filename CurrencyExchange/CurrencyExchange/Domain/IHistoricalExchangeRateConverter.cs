namespace CurrencyExchange.Domain;

public interface IHistoricalExchangeRateConverter
{
    public IEnumerable<DateOnly> AvailableDates();

    IEnumerable<string> AvailableCurrencies(DateOnly date);

    decimal ExchangeRate(DateOnly date, string from, string to);

    void Update();
}
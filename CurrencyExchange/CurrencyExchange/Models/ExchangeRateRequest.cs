namespace CurrencyExchange.Models;

public class ExchangeRateRequest
{
    public string From { set; get; } = string.Empty;
    
    public string To { set; get; } = string.Empty;
}
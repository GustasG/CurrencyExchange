namespace CurrencyExchange.Models;

public class HistoricalExchangeRateRequest
{
    public DateOnly Date { set; get; }
    
    public string From { set; get; } = string.Empty;
    
    public string To { set; get; } = string.Empty;
}
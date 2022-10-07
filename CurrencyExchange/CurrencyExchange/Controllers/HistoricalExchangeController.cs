using Microsoft.AspNetCore.Mvc;

using CurrencyExchange.Domain;
using CurrencyExchange.Models;

namespace CurrencyExchange.Controllers;

[ApiController]
[Route("[controller]")]
public class HistoricalExchangeController : ControllerBase
{
    private readonly IHistoricalExchangeRateConverter _exchangeRateConverter;

    public HistoricalExchangeController(IHistoricalExchangeRateConverter exchangeRateService)
    {
        _exchangeRateConverter = exchangeRateService;
    }

    [HttpGet("Dates")]
    public IActionResult Dates()
    {
        return Ok(_exchangeRateConverter.AvailableDates());
    }
    
    [HttpPost("Currencies")]
    public IActionResult Currencies([FromBody] HistoricalExchangeCurrencyRequest currencyRequest)
    {
        return Ok(_exchangeRateConverter.AvailableCurrencies(currencyRequest.Date));
    }

    [HttpPost("Rate")]
    public IActionResult Rate([FromBody] HistoricalExchangeRateRequest rateRequest)
    {
        return Ok(_exchangeRateConverter.ExchangeRate(rateRequest.Date, rateRequest.From, rateRequest.To));
    }
}
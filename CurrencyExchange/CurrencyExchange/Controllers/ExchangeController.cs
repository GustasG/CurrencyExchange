using Microsoft.AspNetCore.Mvc;

using CurrencyExchange.Domain;
using CurrencyExchange.Models;

namespace CurrencyExchange.Controllers;

[ApiController]
[Route("[controller]")]
public class ExchangeController : ControllerBase
{
    private readonly IExchangeRateConverter _exchangeRateConverter;

    public ExchangeController(IExchangeRateConverter exchangeRateService)
    {
        _exchangeRateConverter = exchangeRateService;
    }

    [HttpGet("Currencies")]
    public IActionResult Currencies()
    {
        return Ok(_exchangeRateConverter.AvailableCurrencies());
    }

    [HttpPost("Rate")]
    public IActionResult Rate([FromBody] ExchangeRateRequest request)
    {
        return Ok(_exchangeRateConverter.ExchangeRate(request.From, request.To));
    }
}
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CurrencyExchange.Tests.Api;

public class TestExchangeApi : IClassFixture<TestEnvironmentExchangeApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public TestExchangeApi(TestEnvironmentExchangeApplicationFactory applicationFactory)
    {
        _httpClient = applicationFactory.CreateClient();
    }
    
    [Fact]
    public async Task Test_Requesting_Available_Currencies_Should_Return_Valid_List_Of_Currencies()
    {
        var response = await _httpClient.GetAsync("/Exchange/Currencies");
        var currencies = await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(currencies);
        Assert.Contains("EUR", currencies);
        Assert.Contains("USD", currencies);
    }

    [Theory]
    [InlineData("USD")]
    [InlineData("EUR")]
    public async Task Test_Requesting_Exchange_Rate_Between_Same_Currency_Should_Return_1(string currency)
    {
        var response = await _httpClient.PostAsJsonAsync("/Exchange/Rate", new
        {
            From = currency,
            To = currency
        });
        var rate = await response.Content.ReadFromJsonAsync<decimal>();
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(1.0m, rate);
    }

    [Theory]
    [InlineData("EUR", "USD")]
    [InlineData("USD", "EUR")]
    [InlineData("USD", "GBP")]
    [InlineData("GBP", "USD")]
    public async Task Test_Requesting_Exchange_Rate_Between_Valid_Currencies_Should_Return_Non_Negative_Number(
        string from, string to)
    {
        var response = await _httpClient.PostAsJsonAsync("/Exchange/Rate", new
        {
            From = from,
            To = to
        });
        var rate = await response.Content.ReadFromJsonAsync<decimal>();
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(rate > 0.0m);
    }

    [Theory]
    [InlineData("EUR", "dba")]
    [InlineData("dba", "EUR")]
    [InlineData("dba", "gda")]
    public async Task Test_Requesting_Exchange_With_Invalid_Currency_Should_Return_Error(string from, string to)
    {
        var response = await _httpClient.PostAsJsonAsync("/Exchange/Rate", new
        {
            From = from,
            To = to
        });
        
        Assert.False(response.IsSuccessStatusCode);
    }
}
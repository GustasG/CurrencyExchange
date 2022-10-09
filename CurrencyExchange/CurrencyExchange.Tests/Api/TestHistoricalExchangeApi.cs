using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace CurrencyExchange.Tests.Api;

public class TestHistoricalExchangeApi : IClassFixture<TestEnvironmentExchangeApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public TestHistoricalExchangeApi(TestEnvironmentExchangeApplicationFactory applicationFactory)
    {
        _httpClient = applicationFactory.CreateClient();
    }

    [Fact]
    public async Task Test_Requesting_Available_Dates_Should_Return_Valid_List_With_Dates()
    {
        var dates = await GetAvailableDates();
        
        Assert.NotEmpty(dates);
    }

    [Fact]
    public async Task Test_Requesting_Available_Currencies_At_Last_Available_Date_Should_Return_Valid_List_Of_Currencies()
    {
        var dates = await GetAvailableDates();

        var response = await _httpClient.PostAsJsonAsync("/HistoricalExchange/Currencies", new
        {
            Date = dates[^1]
        });
        var currencies = await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(currencies);
        Assert.Contains("EUR", currencies);
        Assert.Contains("USD", currencies);
    }

    [Theory]
    [InlineData("EUR", "EUR")]
    [InlineData("USD", "USD")]
    public async Task Test_Requesting_Exchange_Rate_At_Last_Available_Date_Between_Same_Currencies_Should_Return_1(string from, string to)
    {
        var dates = await GetAvailableDates();
        var response = await _httpClient.PostAsJsonAsync("/HistoricalExchange/Rate", new
        {
            Date = dates[^1],
            From = from,
            To = to
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
    public async Task
        Test_Requesting_Exchange_Rate_At_Last_Available_Date_Between_Valid_Currencies_Should_Return_Non_Negative_Number(
            string from, string to)
    {
        var dates = await GetAvailableDates();
        var response = await _httpClient.PostAsJsonAsync("/HistoricalExchange/Rate", new
        {
            Date = dates[^1],
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
    public async Task Test_Requesting_Exchange_Rte_At_Last_Available_Date_With_Invalid_Currency_Should_Return_Error(
        string from, string to)
    {
        var dates = await GetAvailableDates();
        var response = await _httpClient.PostAsJsonAsync("/HistoricalExchange/Rate", new
        {
            Date = dates[^1],
            From = from,
            To = to
        });
        
        Assert.False(response.IsSuccessStatusCode);
    }

    private async Task<IList<string>> GetAvailableDates()
    {
        var response = await _httpClient.GetAsync("/HistoricalExchange/Dates");
        var dates = await response.Content.ReadFromJsonAsync<IList<string>>();

        return dates!;
    }
}
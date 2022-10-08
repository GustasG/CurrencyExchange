﻿using System.Net;
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
    public async Task Test_Requesting_Available_Currencies_At_First_Available_Date_Should_Return_Valid_List_Of_Currencies()
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

    private async Task<IList<string>> GetAvailableDates()
    {
        var response = await _httpClient.GetAsync("/HistoricalExchange/Dates");
        var dates = await response.Content.ReadFromJsonAsync<IList<string>>();

        return dates!;
    }
}
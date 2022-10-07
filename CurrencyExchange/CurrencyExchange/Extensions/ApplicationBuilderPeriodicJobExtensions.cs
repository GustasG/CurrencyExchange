using Hangfire;

using CurrencyExchange.Domain;

namespace CurrencyExchange.Extensions;

public static class ApplicationBuilderPeriodicJobExtensions
{
    public static void RegisterRecurringJobs(this IApplicationBuilder app)
    {
        var recuringJobOptions = new RecurringJobOptions
        {
           TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Berlin")
        };

        var recurringJobManager = app.ApplicationServices.GetRequiredService<IRecurringJobManager>();

        recurringJobManager.AddOrUpdate<IExchangeRateConverter>("Update exchange rates", converter =>
            converter.Update(), "15 16 * * *", recuringJobOptions);

        recurringJobManager.AddOrUpdate<IHistoricalExchangeRateConverter>("Update historical exchange rates", converter =>
            converter.Update(), "15 16 * * *", recuringJobOptions);
    }
}
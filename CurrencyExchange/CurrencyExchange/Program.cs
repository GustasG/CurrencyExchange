using Microsoft.AspNetCore.Mvc;

using Hangfire;
using Hangfire.MemoryStorage;

using CurrencyExchange.Domain;
using CurrencyExchange.Extensions;
using CurrencyExchange.Infrastructure.ECB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<JsonOptions>(options => options.JsonSerializerOptions.AddDateOnlyConverters());

builder.Services.AddSingleton<IExchangeRateConverter, EcbExchangeRateConverter>();
builder.Services.AddSingleton<IHistoricalExchangeRateConverter, EcbHistoricalExchangeRateConverter>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHangfire(config =>
{
    config.UseMemoryStorage();
});

builder.Services.AddHangfireServer();

builder.Services.AddCors(p => p.AddPolicy("corsapp", configurePolicy =>
{
    configurePolicy
        .WithOrigins("*")
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard();
}

app.UseCors("corsapp");

app.RegisterRecurringJobs();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Required for tests to run
public partial class Program { }
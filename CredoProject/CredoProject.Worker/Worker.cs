using System.Text.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using CredoProject.Core.Db.Entity;
using CredoProject.Core.Db;
using CredoProject.Core.Repositories;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CredoProject.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    //private readonly ICardRepository _repo;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetStringAsync("https://nbg.gov.ge/gw/api/ct/monetarypolicy/currencies/en/json");

                    List<ExchangeRate> exchangeRates = JsonConvert.DeserializeObject<List<ExchangeRate>>(response);
                    decimal USD = 1;
                    decimal EUR =1;

                    foreach (Currencies currency in exchangeRates[0].currencies)
                    {
                        if (currency.code == "EUR")
                            EUR = currency.rate;
                        else if (currency.code == "USD")
                            USD = currency.rate;
                        _logger.LogInformation($"{currency.code}: {currency.rate}");
                    }
                    await RenewExchangeAsync(EUR, USD);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving currency rates");
            }
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(5000, stoppingToken);
        }
    }

    private async Task RenewExchangeAsync(decimal eUR, decimal uSD)
    {
        var option = new DbContextOptionsBuilder<CredoDbContext>();
        option.UseSqlServer("Server=localhost; Database = CredoProjectDb; MultipleActiveResultSets=True; User Id=sa; Password=HardT0Gue$$Pa$$word; Trusted_Connection=True; integrated security=False; Encrypt=False");
        var db = new CredoDbContext(option.Options);
        var _repo = new CardRepository(db);
        foreach (Currency currencyFrom in Enum.GetValues(typeof(Currency)))
        {
            decimal GEL = 1;
            foreach (Currency currencyTo in Enum.GetValues(typeof(Currency)))
            {
                var exchange = _repo.GetExchangeAsync(currencyFrom, currencyTo).Result;
                var tt = exchange.rate;
            }
        }
    }

    public class Currencies
    {
        public string code { get; set; }
        public decimal rate { get; set; }
    }

    public class ExchangeRate
    {
        public string date { get; set; }
        public List<Currencies> currencies { get; set; }
    }
}
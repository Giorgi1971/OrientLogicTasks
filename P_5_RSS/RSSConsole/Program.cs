using RSSConsole;
using RSSFeedAPI.Db;
using RSSFeedAPI.Db.Entity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Timers;
using System.Security.Policy;
using System.Diagnostics;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Xml;
using Newtonsoft.Json.Linq;

public class Program
{

    public static async Task Main()
    {
        string connectionString = "Server=localhost; Database = RSSFeedNew; User Id=sa; Password=HardT0Gue$$Pa$$word; Trusted_Connection=True; integrated security=False; Encrypt=False;";
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(connectionString);
        var options = optionsBuilder.Options;

        AppDbContext db = new(options);
        StartRSS startRSS = new(db);

        var urlList = await startRSS.GetUrlsAsync();
        Console.WriteLine(urlList.Count + "- number Urls. -- " + DateTime.UtcNow.ToString());

        var result = await Task.WhenAll(urlList.Select(startRSS.BeginFeedsAsync));

        Console.WriteLine("---------------------------------");
        foreach (var item in result)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine(result);

        Console.ReadKey();
    }
}



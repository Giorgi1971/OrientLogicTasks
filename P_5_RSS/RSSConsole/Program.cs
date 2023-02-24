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
        // აქ რატომ ვიმეორებ, ჩემი AppDbContext-იდან ვერ გამოვიყენებ???
        string connectionString = "Server=localhost; Database = RSSFeedNew; User Id=sa; Password=HardT0Gue$$Pa$$word; Trusted_Connection=True; integrated security=False; Encrypt=False;";
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(connectionString);
        var options = optionsBuilder.Options;

        AppDbContext db = new(options);
        StartRSS startRSS = new(db);

        var urlList = await startRSS.GetUrlsAsync();
        //Console.WriteLine(urlList.Count + "- number Urls. -- " + DateTime.UtcNow.ToString());

        var result = await Task.WhenAll(urlList.Select(startRSS.BeginFeedsAsync));
        Console.WriteLine("This is almost Finish");
        var speak = Console.ReadLine();
        Console.WriteLine($"Good {speak}");
    }
}

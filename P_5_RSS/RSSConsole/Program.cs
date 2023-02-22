using RSSConsole;
using RSSFeedAPI.Db;
using RSSFeedAPI.Db.Entity;
using RSSConsole.ConsoleRepository;
using RSSConsole.Service;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Timers;
using System.Security.Policy;
using System.Diagnostics;
using System.ServiceModel.Syndication;
using System.Xml;
using Newtonsoft.Json.Linq;

public class Program
{
    static System.Timers.Timer timer;

    public static async Task Main()
    {
        // Create DbContextOptions using the connection string
        //Stopwatch timer = new Stopwatch();

        //timer = new System.Timers.Timer(100000); // 50 minutes in milliseconds
        //timer.Elapsed += async (sender, e) => await OnTimerElapsedAsync(sender, e);
        //timer.Start();

        //await OnTimerElapsedAsync();
        string connectionString = "Server=localhost; Database = RSSFeedNew; User Id=sa; Password=HardT0Gue$$Pa$$word; Trusted_Connection=True; integrated security=False; Encrypt=False;";
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(connectionString);
        var options = optionsBuilder.Options;

        // Note that the using block is used to ensure that the AppDbContext instance is properly disposed of when it is no longer needed
        AppDbContext db = new(options);
        UrlRepository url = new(db);
        FeedRepository feed = new(db);
        FeedService feedService = new(feed);
        StartRSS startRSS = new(feedService, feed, url, db);

        //var feeds2 = SyndicationFeed.Load(XmlReader.Create("https://dev.to/feed/"));
        //Console.WriteLine(feeds2.LastUpdatedTime);
        // Call the async method you want to run periodically


        var urlList = await startRSS.GetUrlsAsync();
        Console.WriteLine(urlList.Count + DateTime.UtcNow.ToString());

        var result = await Task.WhenAll(urlList.Select(startRSS.ProcessFeedsAsync));
        Console.WriteLine(result);

        Console.ReadKey();

        //foreach (var item in urlList)
        //{
        //    await startRSS.ProcessFeedsAsync(item);
        //}

        //Console.WriteLine("Timer elapsed at {0}", e.SignalTime);
        //await DoAsyncWork();

        //timer.Stop();
        //TimeSpan elapsedTime = timer.Elapsed;

        //Console.WriteLine("Elapsed time: {0}", elapsedTime);

        //Console.WriteLine("Press any key to exit...");
        //Console.ReadKey();
    }


    //private static async Task OnTimerElapsedAsync(object sender, ElapsedEventArgs e)
    //private static async Task OnTimerElapsedAsync()
    //{
    //}

    //private static async Task DoAsyncWork()
    //{
    //    // ამას ვერ ვხვდები კარგად!!!!!! აქ რა უნდა ჩავწერო
    //    // Add your asynchronous code here
    //    await Task.Delay(1000); // For example, wait for 1 second
    //    Console.WriteLine("Async work done");
    //}
}

//using System;
//using System.Threading.Tasks;
//using System.Timers;

//public class Program
//{
//    static Timer timer;

//    public static async Task Main()
//    {
//        timer = new Timer(300000); // 5 minutes in milliseconds
//        timer.Elapsed += async (sender, e) => await OnTimerElapsedAsync(sender, e);
//        timer.Start();

//        Console.WriteLine("Press any key to exit...");
//        Console.ReadKey();
//    }

//    private static async Task OnTimerElapsedAsync(object sender, ElapsedEventArgs e)
//    {
//        // Call the async method you want to run periodically
//        Console.WriteLine("Timer elapsed at {0}", e.SignalTime);
//        await DoAsyncWork();
//    }

//    private static async Task DoAsyncWork()
//    {
//        // Add your asynchronous code here
//        await Task.Delay(1000); // For example, wait for 1 second
//        Console.WriteLine("Async work done");
//    }
//}

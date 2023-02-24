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
using System.Threading.Tasks;
using System.Collections.Generic;


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

        var rssUrls = await startRSS.GetUrlsAsync();

        // Set the number of URLs to retrieve at a time
        int pageSize = 4;

        // Set the delay between each page retrieval
        TimeSpan delay = TimeSpan.FromSeconds(10);

        // Create a loop to retrieve pages of URLs
        int page = 0;
        while (true)
        {
            // Retrieve the next page of URLs
            var urls = GetUrls(rssUrls, pageSize, page);

            // Do something with the URLs
            foreach (var url in urls)
            {
                // Retrieve the feeds for the URL
                var feeds = startRSS.BeginFeeds(url);
                if (feeds == null)
                    continue;
                // Do something with the feeds
                foreach (var feed in feeds)
                {
                    await startRSS.AddFeedsFromsyndicatedFeedsAsync(feed, url.WebSiteEntityId);
                    //Console.WriteLine(feed.Title.Text.Trim());
                    // Process the feed
                }
                Console.WriteLine($"Finished url {url.WebSiteEntityId}");
            }
            Console.WriteLine("delay 10 second");
            // Wait for the delay before retrieving the next page
            await Task.Delay(delay);

            // Increment the page counter
            page++;
        }

        // Method to retrieve a page of URLs
        IEnumerable<WebSiteEntity> GetUrls(List<WebSiteEntity> urls, int pageSize, int page)
        {
            // Skip the URLs on previous pages
            var skippedUrls = urls.Skip(pageSize * page);

            // Take the next page of URLs
            var pageUrls = skippedUrls.Take(pageSize);

            return pageUrls;
        }
    }
}

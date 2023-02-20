using Newtonsoft.Json;
using RSSConsole;
using RSSFeedAPI.Db;
using RSSConsole.ConsoleRepository;
using RSSFeedAPI.Db.Entity;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using RSSConsole.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

string connectionString = "Server=localhost; Database = RSSFeedNew; User Id=sa; Password=HardT0Gue$$Pa$$word; Trusted_Connection=True; integrated security=False; Encrypt=False;";

// Create DbContextOptions using the connection string
var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionsBuilder.UseSqlServer(connectionString);
var options = optionsBuilder.Options;

// Note that the using block is used to ensure that the AppDbContext instance is properly disposed of when it is no longer needed
AppDbContext db = new AppDbContext(options);
UrlRepository url = new UrlRepository(db);
UrlService urlService = new UrlService(url);
FeedService feedService = new FeedService();
StartRSS start = new StartRSS(feedService, urlService);


var urlList = start.getUrls();
Console.WriteLine("First step is good");
Console.WriteLine("Recieved url-list");
start.FetchUrls(urlList);
Console.WriteLine("All Fine!");


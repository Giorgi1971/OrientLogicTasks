using Newtonsoft.Json;
using RSSConsole;
using RSSFeedAPI.Db;
using RSSFeedAPI.Db.Entity;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Text.Json;

string feedUrl = "https://feed.infoq.com/";


FetchData fetchData = new FetchData();
var listFeeds = fetchData.FetchFromUrl(feedUrl);

var i = 1;
foreach (var item in listFeeds)
{
    Console.WriteLine(i + ". " + item.Title);
    i++;
}

//Console.ReadKey();

var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://localhost:7096/");
httpClient.DefaultRequestHeaders.Accept.Clear();
httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


foreach (var item in listFeeds) {

    var json = System.Text.Json.JsonSerializer.Serialize(item);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    var response = await httpClient.PostAsync("api/Feed", content);
    if (response.IsSuccessStatusCode)
    {
        var responseJson = await response.Content.ReadAsStringAsync();
        var myModel = JsonConvert.DeserializeObject<FeedEntity>(responseJson);
        Console.WriteLine($"{myModel.Title} - {myModel.Description} ({myModel.CreateAt})");
    }
    else
    {
        Console.WriteLine($"Failed to retrieve data: {response.ReasonPhrase}");
    }
}
Console.WriteLine("Looks Good");
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using ConsoleProject.MM0dels;
using A

public class Program
{
    
    static HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        string feedUrl = "https://feed.infoq.com/";
        XmlDocument rssDoc = new XmlDocument();
        rssDoc.Load(feedUrl);
        XmlNodeList rssItems = rssDoc.SelectNodes("rss/channel/item");

        foreach (XmlNode rssItem in rssItems)
        {
            string title = rssItem.SelectSingleNode("title").InnerText;
            string description = rssItem.SelectSingleNode("description").InnerText;
            string pubDate = rssItem.SelectSingleNode("pubDate").InnerText;
            Console.WriteLine("Title: {0}\nDescription: {1}\nPublication Date: {2}\n", title, description, pubDate);
        }

        //Console.ReadLine();

        var ma1 = new MyApiModel { Name = "Gio", Age = 51 };
        var ma2 = new MyApiModel { Name = "Nino", Age = 45 };

        var apiUrl = "https://localhost:7047/api/Feed/";
        var response = await client.GetAsync(apiUrl);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Feeds>(data);
            Console.WriteLine($"Result: {result}");
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
        }
    }

    //static async Task Main2(string[] args)
    //{
    //    var httpClient = new HttpClient();
    //    httpClient.BaseAddress = new Uri("https://localhost:7047");
    //    httpClient.DefaultRequestHeaders.Accept.Clear();
    //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    //    var response = await httpClient.GetAsync("api/Feed/");
    //    if (response.IsSuccessStatusCode)
    //    {
    //        var content = await response.Content.ReadAsStringAsync();
    //        var myModels = JsonConvert.DeserializeObject<List<MyApiModel>>(content);
    //        foreach (var myModel in myModels)
    //        {
    //            Console.WriteLine($"{myModel.Name} ({myModel.Age})");
    //        }
    //    }
    //    else
    //    {
    //        Console.WriteLine($"Failed to retrieve data: {response.ReasonPhrase}");
    //    }
    //}
}

public class MyApiModel
{
    public string Name { get; set; }
    public int Age { get; set; }
}


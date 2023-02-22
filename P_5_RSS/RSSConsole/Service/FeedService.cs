using System.Xml;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using RSSConsole;
using RSSConsole.Service.Validate;
using RSSConsole.ConsoleRepository;
using RSSFeedAPI.Db;
using RSSFeedAPI.Db.Entity;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using System.ServiceModel.Syndication;

namespace RSSConsole.Service
{
    public class FeedService
    {
        private readonly FeedRepository _FeedRepos;

        public FeedService(FeedRepository FeedRepos)
        {
            _FeedRepos = FeedRepos;
        }



        //public List<FeedEntity> FetchFromUrl(WebSiteEntity feedUrl)
        //{
        //    List<FeedEntity> feedEntities = new();

        //    XmlDocument rssDoc = new();
        //    try
        //    {
        //        rssDoc.Load(feedUrl.Url);
        //    }
        //    catch
        //    {
        //        Console.WriteLine($"No Load From {feedUrl.Url}");
        //        return feedEntities;
        //    }

        //    //syndicate

        //    // ეს მეორე ვარიანტი ჯავასკრიპტის კოდების წაშლის.სავარაუდოდ ეს უფრო სწორია!!!
        //    // Assuming "xmlDoc" is the XmlDocument you received from the RSS feed
        //    string xmlString = rssDoc.OuterXml;
        //    string cleanedXmlString = Regex.Replace(xmlString, @"<script[^>]*>[\s\S]*?</script>", string.Empty, RegexOptions.IgnoreCase);
        //    XmlDocument cleanedXmlDoc = new XmlDocument();
        //    cleanedXmlDoc.LoadXml(cleanedXmlString);

        //    XmlNodeList rssItems = rssDoc.SelectNodes("rss/channel/item");
        //    var i = 0;
        //    foreach (XmlNode rssItem in rssItems)
        //    {
        //        if (i == 3)
        //            break;
        //        i++;
        //        var checkTitle = rssItem.SelectSingleNode("title").InnerText;
        //        Console.WriteLine(checkTitle);
        //        var TitleList = _FeedRepos.GetFeedsTitleByUrlIdAsync(feedUrl.WebSiteEntityId);
        //        //Console.WriteLine(TitleList);
        //        if (TitleList.Result.Contains(checkTitle))
        //            continue;
        //        var entity = new FeedEntity();
        //        entity.Title = checkTitle;

        //        var text = rssItem.SelectSingleNode("description").InnerText.Replace("\"", "'").ToString();
        //        // Assuming "text" is the string you received from the RSS feed
        //        string cleanedText = Regex.Replace(text, @"<script[^>]*>[\s\S]*?</script>", string.Empty, RegexOptions.IgnoreCase);

        //        entity.Description = cleanedText;
        //        entity.CreateAt = DateTime.Now;
        //        //if (DateTime.TryParse(rssItem.SelectSingleNode("updated").InnerText, out dateResult))
        //        //{
        //        //    entity.CreateAt = dateResult;
        //        //};
        //        //entity.Author = rssItem.SelectSingleNode("author").InnerText;
        //        entity.WebSiteEntityId = feedUrl.WebSiteEntityId;
        //        if (entity == null)
        //            Console.WriteLine($"Null is entity {entity.Title}");
        //        else
        //        {
        //            feedEntities.Add(entity);
        //        }
        //    }
        //    return feedEntities;
        //}

        //public HttpClient httpClient()
        //{
        //    var httpClient = new HttpClient();
        //    var _Validate = new ValidateFeedEntity();
        //    httpClient.BaseAddress = new Uri("https://localhost:7096/");
        //    httpClient.DefaultRequestHeaders.Accept.Clear();
        //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    return httpClient;
        //}

        //public async Task PutDataInDb(FeedEntity item, HttpClient _httpClient)
        //{
        //    var json = System.Text.Json.JsonSerializer.Serialize(item);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    var response = await _httpClient.PostAsync("api/Feed", content);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var responseJson = await response.Content.ReadAsStringAsync();
        //        var myModel = JsonConvert.DeserializeObject<FeedEntity>(responseJson);
        //        Console.WriteLine($"{myModel.Title} - {myModel.Description} ({myModel.CreateAt})");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Failed to retrieve data: {response.ReasonPhrase}");
        //    }
        //}
    }
}


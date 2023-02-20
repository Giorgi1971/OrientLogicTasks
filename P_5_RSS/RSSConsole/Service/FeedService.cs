using System;
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

namespace RSSConsole.Service
{
    public class FeedService
    {
        public List<FeedEntity> FetchFromUrl(WebSiteEntity feedUrl)
        {
            List<FeedEntity> feedEntities = new List<FeedEntity>();

            XmlDocument rssDoc = new XmlDocument();
            try
            {
                rssDoc.Load(feedUrl.Url);
            }
            catch
            {
                Console.WriteLine($"No Load From {feedUrl.Url}");
                return feedEntities;
            }
            XmlNodeList rssItems = rssDoc.SelectNodes("rss/channel/item");
            
            foreach (XmlNode rssItem in rssItems)
            {
                DateTime dateResult;
                var entity = new FeedEntity();
                entity.Title = rssItem.SelectSingleNode("title").InnerText;
                entity.Description = rssItem.SelectSingleNode("description").InnerText.Replace("\"", "'").ToString();
                entity.CreateAt = DateTime.Now;
                //if (DateTime.TryParse(rssItem.SelectSingleNode("updated").InnerText, out dateResult))
                //{
                //    entity.CreateAt = dateResult;
                //};
                //entity.Author = rssItem.SelectSingleNode("author").InnerText;
                entity.WebSiteEntityId = feedUrl.WebSiteEntityId;
                if (entity == null)
                    Console.WriteLine($"Null is entity {entity.Title}");
                else
                {
                    feedEntities.Add(entity);
                }
            }
            return feedEntities;
        }

        public HttpClient httpClient()
        {
            var httpClient = new HttpClient();
            var _Validate = new ValidateFeedEntity();
            httpClient.BaseAddress = new Uri("https://localhost:7096/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }

        public async void PutDataInDb(FeedEntity item, HttpClient _httpClient)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Feed", content);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var myModel = JsonConvert.DeserializeObject<FeedEntity>(responseJson);
                //Console.WriteLine($"{myModel.Title} - {myModel.Description} ({myModel.CreateAt})");
            }
            else
            {
                Console.WriteLine($"Failed to retrieve data: {response.ReasonPhrase}");
            }
        }
    }
}


using System;
using RSSConsole.Service;
using RSSFeedAPI.Db.Entity;
using System.Xml;

namespace RSSConsole
{
    public class StartRSS
    {
        private readonly FeedService _feedServ;
        public StartRSS(FeedService service)
        {
            _feedServ = service;
        }

        public List<string> getUrlStringsFromRepository()
        {
            return getUrlStringsFromRepository();
        }

        public void FetchUrls(List<string> urls)
        {
            foreach (var url in urls)
            {
                var feededUrl = FetchFromUrl(url);
            }
        }

        public List<FeedEntity> FetchFromUrl(string feedUrl)
        {
            List<FeedEntity> feedEntities = new List<FeedEntity>();

            XmlDocument rssDoc = new XmlDocument();
            rssDoc.Load(feedUrl);
            XmlNodeList rssItems = rssDoc.SelectNodes("rss/channel/item");

            foreach (XmlNode rssItem in rssItems)
            {
                var entity = new FeedEntity();
                entity.Title = rssItem.SelectSingleNode("title").InnerText;
                entity.Description = rssItem.SelectSingleNode("description").InnerText.Replace("\"", "'").ToString();
                entity.CreateAt = DateTime.Now;
                entity.WebSiteEntityId = 1;
                Console.WriteLine("Title: {0}\nDescription: {1}\nPublication Date: {2}\n", entity.Title, entity.Description, entity.CreateAt);
                if (entity == null)
                    Console.WriteLine("Null is entity");
                else
                {
                    feedEntities.Add(entity);
                    //_db.Feeds.Add(entity);
                    //_db.SaveChanges();
                }

            }
            return feedEntities;

        }
    }


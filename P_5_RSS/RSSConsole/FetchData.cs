using System.Net;
using System;
using System.Xml;
using RSSFeedAPI.Db;
using RSSFeedAPI.Db.Entity;

namespace RSSConsole
{
    public class FetchData
    {
        private readonly AppDbContext _db;

        //public FetchData(AppDbContext db)
        //    {
        //        _db = db;
        //    }


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
}



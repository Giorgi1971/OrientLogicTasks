using System;
using RSSConsole.Service;
using RSSConsole.ConsoleRepository;
using RSSFeedAPI.Db;
using RSSFeedAPI.Db.Entity;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RSSConsole
{
    public class StartRSS
    {
        private readonly FeedService _feedServ;
        private readonly UrlRepository _urlRepo;
        private readonly AppDbContext _db;
        private readonly FeedRepository _feedRepo;

        public StartRSS(FeedService service, FeedRepository feedRepo, UrlRepository urlRepository, AppDbContext db)
        {
            _feedServ = service;
            _urlRepo = urlRepository;
            _db = db;
            _feedRepo = feedRepo;
        }

        // FetchBySindicationAsync
        public async Task<string> ProcessFeedsAsync(WebSiteEntity feedUrl)
        {
            var feeds = new SyndicationFeed();
            try
            {
                var url = feedUrl.Url;
                Console.WriteLine($"Fetchingggg {feedUrl.WebSiteEntityId}");
                feeds = SyndicationFeed.Load(XmlReader.Create(url));
            }
            catch
            {
                Console.WriteLine($"Dont fwtch web Id - {feedUrl.WebSiteEntityId}");
                return "Hello";
            }
            // ვამოწმებთ ამ ulr-ze განახლება იყო თუ არა
            // ამაზე ერორს აგდებს:
            //if (feeds.LastUpdatedTime == feedUrl.LastUpdated)
            //    return;
            // თუ იყო ვნახულობთ ახალი სათაურს და ვამატებსთ ფიდს

            //var feedsTitles = await _feedRepo.GetFeedsTitleByUrlIdAsync(feedUrl.WebSiteEntityId);
            var feedsTitles = _db.Feeds.Where(x => x.WebSiteEntityId == feedUrl.WebSiteEntityId);
            Console.WriteLine(feedsTitles.Count());
            if (feeds == null)
                return "sdasdas";
            return "end";
            //var i = 0;
            //foreach (var item in feeds.Items)
            //{
            //    // ფიდის დამატების პირობები:
            //    // სათაური არ უნდა იყოს ცარიელი
            //    if (i >= 2)
            //        break;
            //    i++;
            //    if (!string.IsNullOrEmpty(item.Title.Text))
            //    {
            //        // სათაური არ უნდა გვქონდეს უკვე Feed-ებში.
            //        if (!feedsTitles.Contains(item.Title.Text))
            //        {
            //            var feedEntity = new FeedEntity() { Title = "NoTitle" };

            //            feedEntity.Title = item.Title.Text.Trim();
            //            // 1. წავშალოთ ჯავასკრიპტის კოდი ტექსტში
            //            string rssFeedText = item.Summary.Text;
            //            string cleanedText = Regex.Replace(rssFeedText, @"<script\b[^>]*>(.*?)</script>", "", RegexOptions.IgnoreCase);
            //            feedEntity.Description = cleanedText.Trim();
            //            feedEntity.Author = item.Authors.FirstOrDefault()?.Name??"Unknoun";
            //            feedEntity.CreateAt = item.LastUpdatedTime.DateTime;
            //            feedEntity.WebSiteEntityId = feedUrl.WebSiteEntityId;
            //            var fent = await _db.Feeds.AddAsync(feedEntity);
            //            var ttt = await _db.SaveChangesAsync();
            //            // ვიწყებთ tag-ის ჩაწერას:
            //            //var tags = await _urlRepo.GetAllTagsAsync();
            //            //string[] tagList = new string[5];
            //            //foreach (var tag in tags)
            //            //{
            //            //    // 2. შევამოწმოთ 5-ზე მეტი ტეგი ხომ არ აქვს
            //            //    if (tagList.Length > 5)
            //            //        break;
            //            //    // 3. დავამატოთ ახალი ტეგები

            //            //    if (cleanedText.Contains(tag.TagTitle))
            //            //    {
            //            //        var feedtag = new FeedTag();
            //            //        feedtag.FeedEntityId = fent.Entity.FeedEntityId;
            //            //        feedtag.TagEntityId = tag.TagEntityId;
            //            //        await _db.FeedTag.AddAsync(feedtag);
            //            //        await _db.SaveChangesAsync();
            //            //        tagList.Append(tag.TagTitle);
            //            //    }
            //            //}
            //}
            //}
        }
        //Console.WriteLine("Feed title: {0}", feeds.Title.Text);
        //Console.WriteLine("RSS feed fetched");
        //}


        public async Task<List<WebSiteEntity>> GetUrlsAsync()
        {
            var result = await _db.Urls
                .Where(x => x.WebSiteEntityId < 4)
                .ToListAsync();
            return result;
        }

        //public async Task FetchUrlsAsync(List<WebSiteEntity> urls)
        //{
        //    var httpClient = _feedServ.httpClient();
        //    foreach (var url in urls)
        //    {
        //        List<FeedEntity> feedEntities = _feedServ.FetchFromUrl(url);
        //        var j = 0;
        //        foreach (var feedEntity in feedEntities)
        //        {
        //            if (j == 3)
        //                break;
        //            await _feedServ.PutDataInDb(feedEntity, httpClient);
        //            j++;
        //        }
        //    }
        //}
    }
}


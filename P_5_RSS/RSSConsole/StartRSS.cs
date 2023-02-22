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
        private readonly AppDbContext _db;

        public StartRSS(AppDbContext db)
        {
            _db = db;
        }

        public async Task<string> BeginFeedsAsync(WebSiteEntity feedUrl)
        {
            SyndicationFeed syndicatedFeeds;

            try
            {
                syndicatedFeeds = SyndicationFeed.Load(XmlReader.Create(feedUrl.Url));
                Console.WriteLine($"Fetched - {feedUrl.WebSiteEntityId}");
            }
            catch
            {
                Console.WriteLine($"Dont fetched - {feedUrl.WebSiteEntityId}");
                return "Hello";
            }

            var feeds = GetFeedsByUrlIdsAsync(feedUrl.WebSiteEntityId).Result;
            var feedTitles = feeds.Select(obj => ((FeedEntity)obj).Title).ToList();

            Console.WriteLine(feedTitles.Count);
            Console.WriteLine(syndicatedFeeds.LastUpdatedTime);
            Console.WriteLine(feedUrl.LastUpdated);
            if (feedTitles.Count != 0)
            {
                // ვამოწმებთ ამ ulr-ze განახლება იყო თუ არა
                //feedUrl.LastUpdated = syndicatedFeeds.LastUpdatedTime.UtcDateTime;
                if (syndicatedFeeds.LastUpdatedTime.DateTime == feedUrl.LastUpdated)
                {
                    return "This Url is Up-to-Date";
                };
                Console.WriteLine(":must be updated");
                //Todo must be updated partially
                // თუ იყო ვნახულობთ ახალი სათაურს და ვამატებსთ ფიდს
            }
            await AddFeedsFromsyndicatedFeedsAsync(syndicatedFeeds, feedTitles, feedUrl.WebSiteEntityId); 
            return "All Ok";
        }


    public async Task AddFeedsFromsyndicatedFeedsAsync(SyndicationFeed syndicatedFeeds, List<string> feedTitles, int urlId)
        {
            var i = 0;
            foreach (var item in syndicatedFeeds.Items)
            {
                // ფიდის დამატების პირობები:
                // სათაური არ უნდა იყოს ცარიელი
                var title = item.Title.Text.Trim();
                if (i >= 3)
                    break;
                i++;
                if (string.IsNullOrEmpty(title))
                    continue;
                // სათაური არ უნდა გვქონდეს უკვე Feed-ებში.
                if (feedTitles.Contains(title))
                    continue;
                var feedEntity = new FeedEntity() { Title = "NoTitle" }; // Title not null aqvs
                feedEntity.Title = title;
                // 1. წავშალოთ ჯავასკრიპტის კოდი ტექსტში
                string rssFeedText = item.Summary.Text.Trim();
                string cleanedText = Regex.Replace(rssFeedText, @"<script\b[^>]*>(.*?)</script>", "", RegexOptions.IgnoreCase);
                feedEntity.Description = cleanedText.Trim();
                feedEntity.Author = item.Authors.FirstOrDefault()?.Name ?? "Unknoun";
                feedEntity.CreateAt = item.PublishDate.DateTime;
                feedEntity.WebSiteEntityId = urlId;
                var fent = await _db.Feeds.AddAsync(feedEntity);
                await _db.SaveChangesAsync();
            }
        }

                    // ვიწყებთ tag-ის ჩაწერას:
                    //var tags = await _urlRepo.GetAllTagsAsync();
                    //string[] tagList = new string[5];
                    //foreach (var tag in tags)
                    //{
                    //    // 2. შევამოწმოთ 5-ზე მეტი ტეგი ხომ არ აქვს
                    //    if (tagList.Length > 5)
                    //        break;
                    //    // 3. დავამატოთ ახალი ტეგები

                    //    if (cleanedText.Contains(tag.TagTitle))
                    //    {
                    //        var feedtag = new FeedTag();
                    //        feedtag.FeedEntityId = fent.Entity.FeedEntityId;
                    //        feedtag.TagEntityId = tag.TagEntityId;
                    //        await _db.FeedTag.AddAsync(feedtag);
                    //        await _db.SaveChangesAsync();
                    //        tagList.Append(tag.TagTitle);


        public async Task<List<WebSiteEntity>> GetUrlsAsync()
        {
            var result = await _db.Urls
                .Where(x => x.WebSiteEntityId < 4)
                .ToListAsync();
            return result;
        }

        public async Task<List<FeedEntity>> GetFeedsByUrlIdsAsync(int Id)
        {
            var result = await _db.Feeds
                .Where(x => x.WebSiteEntityId == Id)
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


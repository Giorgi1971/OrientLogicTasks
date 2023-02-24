using System;
using RSSFeedAPI.Db;
using RSSFeedAPI.Db.Entity;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RSSConsole.Service;

namespace RSSConsole
{
    public partial class StartRSS
    {
        private readonly AppDbContext _db;

        public StartRSS(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<SyndicationItem>? BeginFeeds(WebSiteEntity feedUrl)
        {
            SyndicationFeed syndicatedFeeds;
            try
            {
                syndicatedFeeds = SyndicationFeed.Load(XmlReader.Create(feedUrl.Url));
                //Console.WriteLine($"Fetched - {feedUrl.WebSiteEntityId}");
            }
            catch
            {
                //Console.WriteLine($"Dont fetched - {feedUrl.WebSiteEntityId}");
                return null;
            }
            return syndicatedFeeds.Items;
        }

        //    var feeds = GetFeedsByUrlIdsAsync(feedUrl.WebSiteEntityId).Result;
        //    var feedTitles = feeds.Select(obj => obj.Title).ToList();

        //    Console.WriteLine("BeginFeedsAsync "+feedUrl.WebSiteEntityId);
        //    if (feedTitles.Count != 0)
        //    {
        //        // აქ სადღაც უნდა ვინახავსე სინდიკატის ბოლო განახლების თარიღს webUrlEntity-ში ბაზაში.!!!!!!!!!!!!!!!!!!
        //        // ისე ამის შემოწმებას აზრი არ აქვს. და ბოლოშიც როცა ნაწილობრივ განვაახლებ შეიძლება მაშინაც იყოს საჭირო
        //        if (syndicatedFeeds.LastUpdatedTime.DateTime == feedUrl.LastUpdated)
        //        {
        //            return "This Url is Up-to-Date";
        //        };
        //        //Console.WriteLine(":must be updated");
        //        //Todo must be updated partially
        //        // თუ იყო ვნახულობთ ახალი სათაურს და ვამატებსთ ფიდს
        //        // ----- ეს მგონი უკვე გაკეთებულია
        //    }
        //    await AddFeedsFromsyndicatedFeedsAsync(syndicatedFeeds, feedTitles, feedUrl.WebSiteEntityId); 
        //    return "All Ok";
        //}

        //public async Task AddFeedsFromsyndicatedFeedsAsync(SyndicationFeed syndicatedFeeds, List<string> feedTitles, int urlId)
        //    {
        //        //var i = 0;
        //        Console.WriteLine("AddFeedsFromsyndicatedFeedsAsync - "+urlId);
        //        foreach (var item in syndicatedFeeds.Items)
        //        {
        // ფიდის დამატების პირობები:
        public async Task AddFeedsFromsyndicatedFeedsAsync(SyndicationItem item, int urlId)
        {

            var title = item.Title.Text.Trim();
            //if (i >= 4)
            //break;
            //i++;
            //if (string.IsNullOrEmpty(title))
            //continue;
            // სათაური არ უნდა გვქონდეს უკვე Feed-ებში.
            //if (feedTitles.Contains(title))
            //continue;
            var feedEntity = new FeedEntity() { Title = "NoTitle" }; // Title not null aqvs
            feedEntity.Title = title;
            // 1. წავშალოთ ჯავასკრიპტის კოდი ტექსტში
            try
            {
                string rssFeedText = item.Summary.Text.Trim();
                //string cleanedText = MyRegex().Replace(rssFeedText, "");
                feedEntity.Description = rssFeedText.Trim();
            }
            catch
            {
                feedEntity.Description = "Cannot Fetch text, from JS code. aslo ";
            }
            feedEntity.Author = item.Authors.FirstOrDefault()?.Name ?? "Unknoun";
            feedEntity.CreateAt = item.PublishDate.DateTime;
            feedEntity.WebSiteEntityId = urlId;
            var fent = await _db.Feeds.AddAsync(feedEntity);
            await _db.SaveChangesAsync();

            var tags = await _db.Tags.ToListAsync();
            string[] tagList = new string[5];
            foreach (var tag in tags)
            {
                if (tagList.Length > 5)
                    break;

                if (feedEntity.Description.Contains(tag.TagTitle))
                {
                    var feedtag = new FeedTag();
                    feedtag.FeedEntityId = fent.Entity.FeedEntityId;
                    feedtag.TagEntityId = tag.TagEntityId;
                    await _db.FeedTag.AddAsync(feedtag);
                    await _db.SaveChangesAsync();
                    var dd = tagList.Append(tag.TagTitle);
                }

                //Console.WriteLine("Add Feeds url " + urlId);
            }
            //Console.WriteLine("There below is task Delay 10000");
            //await Task.Delay(1000);
            //Console.WriteLine("finished - " + urlId);
        }

        public async Task<List<WebSiteEntity>> GetUrlsAsync()
        {
            var result = await _db.Urls
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

        //[GeneratedRegex("<script\\b[^>]*>(.*?)</script>", RegexOptions.IgnoreCase, "en-GE")]
        //private static partial Regex MyRegex();
    }

}
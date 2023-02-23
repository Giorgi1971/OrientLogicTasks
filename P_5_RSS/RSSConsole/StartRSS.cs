using System;
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
                // აქ სადღაც უნდა ვინახავსე სინდიკატის ბოლო განახლების თარიღს webUrlEntity-ში ბაზაში.!!!!!!!!!!!!!!!!!!
                // ისე ამის შემოწმებას აზრი არ აქვს. და ბოლოშიც როცა ნაწილობრივ განვაახლებ შეიძლება მაშინაც იყოს საჭირო
                if (syndicatedFeeds.LastUpdatedTime.DateTime == feedUrl.LastUpdated)
                {
                    return "This Url is Up-to-Date";
                };
                Console.WriteLine(":must be updated");
                //Todo must be updated partially
                // თუ იყო ვნახულობთ ახალი სათაურს და ვამატებსთ ფიდს
                // ----- ეს მგონი უკვე გაკეთებულია
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
                var title = item.Title.Text.Trim();
                if (i >= 4)
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
                try
                {
                string rssFeedText = item.Summary.Text.Trim();
                string cleanedText = Regex.Replace(rssFeedText, @"<script\b[^>]*>(.*?)</script>", "", RegexOptions.IgnoreCase);
                feedEntity.Description = cleanedText.Trim();
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
                        tagList.Append(tag.TagTitle);
                    }
                }
            }
        }


        public async Task<List<WebSiteEntity>> GetUrlsAsync()
        {
            var result = await _db.Urls
                //.Where(x => x.WebSiteEntityId < 7)
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
    }
}


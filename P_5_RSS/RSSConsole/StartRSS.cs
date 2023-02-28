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
using Microsoft.Security.Application;


namespace RSSConsole
{
    public partial class StartRSS
    {
        private readonly AppDbContext _db;

        public StartRSS(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<SyndicationItem>? GetSyndicatedFeedsFromUrl(WebSiteEntity feedUrl)
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
                return null;
            }

            Console.WriteLine("BeginFeedsAsync " + feedUrl.WebSiteEntityId);
            if (!syndicatedFeeds.Items.Any()) return null;
            if (syndicatedFeeds.LastUpdatedTime.DateTime == feedUrl.LastUpdated) return null;
            feedUrl.LastUpdated = syndicatedFeeds.LastUpdatedTime.DateTime;
            Console.WriteLine(":must be updated");
            return syndicatedFeeds.Items;
        }

        //        {
        // ფიდის დამატების პირობები:
        public async Task AddFeedFromsyndicatedFeedsAsync(SyndicationItem item, int urlId)
        {
            var oldFeeds = GetFeedsByUrlIdsAsync(urlId).Result;
            var oldFeedTitles = oldFeeds.Select(obj => obj.Title).ToList();

            var title = item.Title.Text.Trim();
            // სათაური არ უნდა გვქონდეს უკვე Feed-ებში.
            if (oldFeedTitles.Contains(title)) return;
            
            var feedEntity = new FeedEntity() { Title = "NoTitle" }; // Title not null aqvs
            feedEntity.Title = title;
            // 1. წავშალოთ ჯავასკრიპტის კოდი ტექსტში
            try
            {
                string rssFeedText = item.Summary.Text.Trim();

                // sanitize the HTML content of the feed item
                //var sanitizedHtml = Sanitizer.GetSafeHtml(item.Content, new string[] { "a", "img", "strong", "em", "u" });
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
            List<string> tags;
            try
            {
                tags = await _db.Tags.Select(x => x.TagTitle).ToListAsync();
            }
            catch
            {
                tags = new List<string>();
            }
            //var allTagsTitle = tags.Select(x => x.TagTitle).ToList();
            string[] tagList = new string[5];
            var feedTagsFromUrl = item.Categories.ToList();
            if (feedTagsFromUrl.Count < 1) Console.WriteLine("No categories");
            foreach (var tag in feedTagsFromUrl)
            {
                Console.WriteLine(tag.Name);
                if (tagList.Length > 5)
                    break;

                var feedtag = new FeedTag();
                if (!tags.Contains(tag.Name))
                {
                    TagEntity newCategory = new TagEntity { TagTitle = tag.Name };
                    await _db.Tags.AddAsync(newCategory);
                    await _db.SaveChangesAsync();
                }
                feedtag.FeedEntityId = fent.Entity.FeedEntityId;
                var dd = await _db.Tags.FirstOrDefaultAsync(x => x.TagTitle == tag.Name);
                feedtag.TagEntityId = dd.TagEntityId;
                await _db.FeedTag.AddAsync(feedtag);
                await _db.SaveChangesAsync();
                var ddd = tagList.Append(tag.Name);
            Console.WriteLine("Add Feeds url " + urlId);
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
    }
}

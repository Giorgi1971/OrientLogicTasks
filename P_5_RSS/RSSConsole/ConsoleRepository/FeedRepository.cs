using System;
using RSSFeedAPI.Db;
using RSSFeedAPI.Db.Entity;

namespace RSSConsole.ConsoleRepository
{
    public class FeedRepository
    {
        private readonly AppDbContext _db;

        public FeedRepository(AppDbContext db)
        {
            _db = db;
        }

        public List<string?> getFeedTextFromRepository()
        {
            return _db.Feeds
                .OrderByDescending(x => x.Title)
                .Select(x => x.Description)
                .ToList();
        }

        public List<FeedEntity> getFeedsFromRepositoryAsync()
        {
            return _db.Feeds
                .OrderByDescending(x => x.Title)
                .ToList();
        }


        public List<FeedEntity> getFeedsRepositoryByUrlId(int Id)
        {
            var result = _db.Feeds
                .OrderByDescending(x => x.Title)
                .Where(x => x.WebSiteEntityId == Id)
                .ToList();
            return result;
        }

        public List<string> getFeedsTitleByUrlId(int Id)
        {
            var result = _db.Feeds
                .OrderByDescending(x => x.Title)
                .Where(x => x.WebSiteEntityId == Id)
                .Select(x => x.Title)
                .ToList();
            return result;
        }
    }
}


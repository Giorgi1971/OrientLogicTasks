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
                .Select(x => x.Description)
                .ToList();
        }

        public List<FeedEntity> getFeedsFromRepositoryAsync()
        {
            return _db.Feeds.ToList();
        }

    }
}


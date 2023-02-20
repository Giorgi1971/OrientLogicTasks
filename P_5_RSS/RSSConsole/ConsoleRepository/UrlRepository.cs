using System;
using RSSFeedAPI.Db;
using RSSFeedAPI.Db.Entity;
namespace RSSConsole.ConsoleRepository
{
    public class UrlRepository
    {
        private readonly AppDbContext _db;

        public UrlRepository(AppDbContext db)
        {
            _db = db;
        }

        public List<string> getUrlStringsFromRepository()
        {
            return _db.Urls
                .Select(x => x.Url)
                .ToList();
        }

        public List<WebSiteEntity> getUrlFromRepositoryAsync()
        {
            return _db.Urls.ToList();
        }

    }
}


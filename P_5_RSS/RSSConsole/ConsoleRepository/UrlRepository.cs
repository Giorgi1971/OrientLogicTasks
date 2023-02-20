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
            var rr = _db.Urls;
            var result = _db.Urls
                .Select(x => x.Url)
                .ToList();
            return result;
        }

        public List<WebSiteEntity> getWebUrlsFromRepository()
        {
            var result = _db.Urls.ToList();
            return result;
        }

    }
}


using System;
using Microsoft.EntityFrameworkCore;
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

        //public async Task<List<TagEntity>> GetAllTagsAsync()
        //{
        //    var result = await _db.Tags.ToListAsync();
        //    return result;
        //}

        //public async Task<List<string>> GetUrlStringsFromRepository()
        //{
        //    var result = await _db.Urls
        //        .Select(x => x.Url)
        //        .ToListAsync();
        //    return result;
        //}

        //public async Task<List<WebSiteEntity>> GetWebUrlsFromRepositoryAsync()
        //{
        //    var result = await _db.Urls.ToListAsync();
        //    return result;
        //}
    }
}


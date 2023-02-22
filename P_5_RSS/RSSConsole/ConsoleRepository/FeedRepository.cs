using System;
using Microsoft.EntityFrameworkCore;
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

        //    public async Task<List<string?>> GetFeedTextFromRepositoryAsync()
        //    {
        //        var result = await _db.Feeds
        //            .OrderByDescending(x => x.Title)
        //            .Select(x => x.Description)
        //            .ToListAsync();
        //        return result;
        //    }

        //    public async Task<List<FeedEntity>> GetFeedsFromRepositoryAsync()
        //    {
        //        var result = await _db.Feeds
        //            .OrderByDescending(x => x.Title)
        //            .ToListAsync();
        //        return result;
        //    }


        //    public async Task<List<FeedEntity>> GetFeedsRepositoryByUrlIdAsync(int Id)
        //    {
        //        var result = await _db.Feeds
        //            .OrderByDescending(x => x.Title)
        //            .Where(x => x.WebSiteEntityId == Id)
        //            .ToListAsync();
        //        return result;
        //    }

        //    public async Task<List<string>> GetFeedsTitleByUrlIdAsync(int Id)
        //    {
        //        var result = await _db.Feeds
        //            .OrderByDescending(x => x.Title)
        //            .Where(x => x.WebSiteEntityId == Id)
        //            .Select(x => x.Title)
        //            .ToListAsync();
        //        return result;
        //    }
    }
}


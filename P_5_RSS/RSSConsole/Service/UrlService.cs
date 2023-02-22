using System;
using RSSFeedAPI.Db.Entity;
using RSSConsole.ConsoleRepository;

namespace RSSConsole.Service
{
    public class UrlService
    {
        private readonly UrlRepository _urlRepos;

        public UrlService(UrlRepository url)
        {
            _urlRepos = url;
        }

        //public async Task<List<string>> GetUrlStringsFromService()
        //{
        //    var result = await _urlRepos.GetUrlStringsFromRepository();
        //    return result;
        //}

        //public async Task<List<WebSiteEntity>> GetUrsFromServiceAsync()
        //{
        //    var result = await _urlRepos.GetWebUrlsFromRepositoryAsync();
        //    return result;
        //}
    }
}


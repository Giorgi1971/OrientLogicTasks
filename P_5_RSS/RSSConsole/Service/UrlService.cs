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

        public List<string> getUrlStringsFromService()
        {
            var result = _urlRepos.getUrlStringsFromRepository();
            return result;
        }

        public List<WebSiteEntity> getUrsFromService()
        {
            var result = _urlRepos.getWebUrlsFromRepository();
            return result;
        }
    }
}


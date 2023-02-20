using System;
using RSSConsole.Service;
using RSSFeedAPI.Db.Entity;
using System.Xml;

namespace RSSConsole
{
    public class StartRSS
    {
        private readonly FeedService? _feedServ;
        private readonly UrlService? _urlServ;

        public StartRSS(FeedService service, UrlService servUrl)
        {
            _feedServ = service;
            _urlServ = servUrl;
        }

        public List<WebSiteEntity> getUrls()
        {
            var result = _urlServ.getUrsFromService();
            return result;
        }

        public void FetchUrls(List<WebSiteEntity> urls)
        {
            var httpClient = _feedServ.httpClient();
            foreach (var url in urls)
            {
                //Console.WriteLine(url.Url);

                var feedEntities = _feedServ.FetchFromUrl(url);
                var j = 0;
                foreach (var feedEntity in feedEntities)
                {
                    if (j == 3)
                        break;
                    _feedServ.PutDataInDb(feedEntity, httpClient);
                    j++;
                }
            }
        }
    }
}


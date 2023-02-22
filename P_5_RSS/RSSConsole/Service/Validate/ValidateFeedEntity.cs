using System;
using RSSConsole.ConsoleRepository;

namespace RSSConsole.Service.Validate
{
    public class ValidateFeedEntity
    {
        private readonly UrlRepository _urlRepos;
        private readonly FeedRepository _feedRepos;

        public ValidateFeedEntity()
        {
        }

        public ValidateFeedEntity(UrlRepository url, FeedRepository feed)
        {
            _urlRepos = url;
            _feedRepos = feed;
        }

        //public bool ValidateTitle()
        //{
        //    _urlRepos.GetWebUrlsFromRepositoryAsync();
        //    return true;
        //}
    }
}


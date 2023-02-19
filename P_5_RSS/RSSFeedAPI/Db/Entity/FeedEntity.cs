using System;
namespace RSSFeedAPI.Db.Entity
{
    public class FeedEntity
    {
        public int FeedEntityId { get; set; }
        //public object MyProperty { get; set; }
        public string Title { get; set; } = null!;
        public string? Author { get; set; }
        public string? Description { get; set; }
        public string? Photo { get; set; }
        public DateTime CreateAt { get; set; }

        public int WebSiteEntityId { get; set; }
        public WebSiteEntity? WebSiteEntity { get; set; }

        public List<FeedTag>? FeedTags { get; set; }
    }
}

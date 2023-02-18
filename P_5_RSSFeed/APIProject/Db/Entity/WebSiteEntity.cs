using System;
namespace APIProject.Db.Entity
{
    public class WebSiteEntity
    {
        public int WebSiteEntityId { get; set; }
        public string Url { get; set; } = null!;
        public string? ContentHash { get; set; }
        public DateTime LastUpdated { get; set; }

        public List<FeedEntity>? Feeds { get; set; }
    }
}

using System;
namespace RSSFeedAPI.Db.Entity
{
    public class FeedTag
    {
        public int FeedEntityId { get; set; }
        public FeedEntity? FeedEntity { get; set; } 

        public int TagEntityId { get; set; }
        public TagEntity? TagEntity { get; set; }
    }
}


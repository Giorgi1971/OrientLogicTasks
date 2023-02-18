using System;
namespace APIProject.Db.Entity
{
    public class FeedTag
    {
        public int FeedEntityId { get; set; }
        public FeedEntity FeedEntity { get; set; } = null!;

        public int TagEntityId { get; set; }
        public TagEntity TagEntity { get; set; } = null!;
    }
}


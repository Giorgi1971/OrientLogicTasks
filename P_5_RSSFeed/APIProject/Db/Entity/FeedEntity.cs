using System;
namespace APIProject.Db.Entity
{
    public class FeedEntity
    {
        public int FeedEntityId { get; set; }
        //public object MyProperty { get; set; }
        public string Title { get; set; } = null!;
        public string? Summary { get; set; }
        public string? Author { get; set; }
        public string? Photo { get; set; }
        public DateTime CreateAt { get; set; }

        public int WebSiteEntityId { get; set; }
        public WebSiteEntity WebSiteEntity { get; set; } = null!;

        public List<FeedTag> FeedTags { get; set; } = null!;
    }
}


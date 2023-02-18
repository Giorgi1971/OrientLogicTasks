using System;
namespace APIProject.Db.Entity
{
    public class TagEntity
    {
        public int TagEntityId { get; set; }
        public string TagTitle { get; set; } = null!;

        public List<FeedTag> FeedTags { get; set; } = null!;
    }
}

using System;
namespace MovieDatabaseAPI.Data.Entity
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Releazed { get; set; }
        public string? MovieDirector { get; set; }
        public Status MovieStatus { get; set; }
        public DateTime CreateAt { get; set; }
    }

    public enum Status
    {
        active,
        deleted
    }
}

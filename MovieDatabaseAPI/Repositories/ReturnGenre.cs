using MovieDatabaseAPI.Data.Entity;

namespace MovieDatabaseAPI.Repositories
{
    public class ReturnGenre
    {
        public int movieId2 { get; set; }
        public Status st { get; set; }
        public string movie { get; set; }
        public List<MovieGenre> gg { get; set; }
    }
}
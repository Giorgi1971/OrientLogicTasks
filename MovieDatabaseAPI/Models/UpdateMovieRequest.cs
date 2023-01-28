using System;
using MovieDatabaseAPI.Data.Entity;
namespace MovieDatabaseAPI.Models
{
    public class UpdateMovieRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public DateTime Released { get; set; }
    }
}


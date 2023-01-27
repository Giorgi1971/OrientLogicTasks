using System;
using MovieDatabaseAPI.Data.Entity;
namespace MovieDatabaseAPI.Models
{
    public class UpdateMovieRequest
    {
        public Movie Movie { get; set; }
    }
}


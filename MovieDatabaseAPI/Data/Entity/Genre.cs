using System;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabaseAPI.Data.Entity
{
    public class Genre
    {
        public int Id { get; set; }
        public string GenreName { get; set; }

        public List<MovieGenre> MovieGenres { get; set; }
    }
}



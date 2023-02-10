using System;
using System.ComponentModel.DataAnnotations;

using MovieDatabaseAPI.Repositories;

namespace MovieDatabaseAPI.Models
{
    public class GetSearchedMoviesRequest
    {
        public FilterMovie? Filter { get; set; }
        [Range(1,100)]
        public int pageSize { get; set; }
        public int pageIndex { get; set; }
    }

    // ამის ცალკე დაწერას რამე აზრი თუ აქვს??
    public class FilterMovie
    {
        public string? InTitle { get; set; }
        public string? InDescription { get; set; }
        public string? InMovieDirector { get; set; }
        public int InReleasedDate { get; set; }
    }
}

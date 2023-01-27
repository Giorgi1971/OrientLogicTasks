using System;
using MovieDatabaseAPI.Repositories;

namespace MovieDatabaseAPI.Models
{
    public class GetSearchedMoviesRequest
    {
        public FilterMovie Filter { get; set; }
        public int pageSize { get; set; }
        public int pageIndex { get; set; }
    }
}

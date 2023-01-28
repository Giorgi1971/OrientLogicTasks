using System;
using MovieDatabaseAPI.Repositories;

namespace MovieDatabaseAPI.Models
{
    public class GetSearchedMoviesRequest2
    {
        public string FilterTitle { get; set; }
        public string FilterDescription { get; set; }
        //public int pageSize { get; set; }
        //public int pageIndex { get; set; }
    }
}

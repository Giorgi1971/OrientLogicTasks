using System;
using MovieDatabaseAPI.Repositories;

namespace MovieDatabaseAPI.Models
{
    public class GetSearchedMoviesRequest2
    {
        public string Filter1 { get; set; }
        public string Filter2 { get; set; }
        //public int pageSize { get; set; }
        //public int pageIndex { get; set; }
    }
}

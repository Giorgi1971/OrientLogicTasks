﻿using System;
using MovieDatabaseAPI.Data.Entity;
namespace MovieDatabaseAPI.Models
{
    public class CreateMovieRequest
    {
        public Movie Movie { get; set; }
    }
}


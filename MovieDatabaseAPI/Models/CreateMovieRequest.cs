using System;
using MovieDatabaseAPI.Data.Entity;
namespace MovieDatabaseAPI.Models
{
    // CreateMovieRequest იყენებს Movie Entity-ს და რა აზრი აქვს CreateMovieRequest ის შექმნას
    // ჯობია ცალკე ქონდეს თავისი აღწერა.
    // პლიუს კიდე Create -ის დროს არ უნდა გადმოგცეთ სხვამ Movie-ის Id ეს გასაკეთებელია თქვენით
    // CreateMovieRequests არ უნდა ქონდეს CreatedAt და Id ფროფერთი
    public class CreateMovieRequest
    {
        public Movie Movie { get; set; }
    }
}


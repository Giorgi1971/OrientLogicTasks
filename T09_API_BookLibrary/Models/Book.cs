﻿using System;
namespace T09_API_BookLibrary.Models
{
    public class Book
    {
        private static int _id = 1;
        public int Id { get; }
        public int ShelfId { get; set; }
        public string? Title { get; set; }
        public string? ISBN { get; set; }
        public string? Description { get; set; }

        public Book()
        {
            Id = _id++;
        }
    }
}


using System;

namespace T09_API_BookLibrary.Models
{
    public class Shelf
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Book> Books { get; set; }

        public Shelf()
        {
            Books = new List<Book>();
        }
    }
}


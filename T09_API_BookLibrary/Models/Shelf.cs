using System;

namespace T09_API_BookLibrary.Models
{
    public class Shelf
    {
        private static int _id = 1;
        public int Id { get; }
        public string? Name { get; set; }
        public List<Book> ShelfBooks { get; }

        public Shelf()
        {
            Id = _id++;
            ShelfBooks = new List<Book>();
        }
    }
}

using System;
namespace BooksInShelf.Models
{
    public class Book
    {
        static int _pk = 1;
        public static readonly List<Book> books = new List<Book> { };
        public int Id { get; set; }
        public string Title { get; set; }
        private int shelfId;
        public int ShelfId {
            get { return shelfId; }
            set
            {
                if (value < Shelf.Shelfs.Count)
                    shelfId = value;
            }
        }

        public Book(string title, int shelfId)
        {
            Id = _pk++;
            Title = title;
            ShelfId = shelfId;
            Shelf.Shelfs[shelfId-1].shelfBooks.Add(this);
            books.Add(this);
        }

        public void Display()
        {
            Console.WriteLine($"Book - {Title}\tid-{Id} in shelf - {Shelf.Shelfs[ShelfId-1]} id {shelfId}.");
        }
    }
}

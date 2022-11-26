using System;
namespace BooksInShelf.Models
{
    public class Shelf
    {
        static int _id = 1;
        public static List<Shelf> Shelfs = new List<Shelf>();
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> shelfBooks { get; set; }

        public Shelf(string name)
        {
            Id = _id++;
            Name = name;
            shelfBooks = new List<Book> { };
            Shelfs.Add(this);
        }

        public void DisplayShelf()
        {
            Console.WriteLine($"\t{Id} Shelf '{Name}' -  contains {shelfBooks.Count} book");
            foreach (var item in shelfBooks)
            {
                Console.WriteLine($"\t\tBook '{item.Title}' id - {item.Id}");
            }
        }
    }
}

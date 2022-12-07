using System;
using T09_API_BookLibrary.Requests;
using T09_API_BookLibrary.Models;

namespace T09_API_BookLibrary.Services
{
    public class ShelfService
    {
        public static List<Shelf> Shelves { get; set; }

        static ShelfService()
        {
            Shelves = new List<Shelf>();
            var shelf1 = new Shelf { Name = "Classic" };
            var shelf2 = new Shelf { Name = "Fantasy" };
            var shelf3 = new Shelf { Name = "Fiction" };
            Shelves.Add(shelf1);
            Shelves.Add(shelf2);
            Shelves.Add(shelf3);
        }

        public Shelf Create(CreateShelfRequest request)
        {
            var shelf = new Shelf { Name = request.Name };
            Shelves.Add(shelf);
            return shelf;
        }

        public Shelf? Rename(RenameShelfRequest request)
        {
            var shelf = Shelves.Find(s => s.Id == request.ShelfdId);
            if (shelf == null)
            {
                return null;
            }
            shelf.Name = request.Name;
            return shelf;
        }

        public Shelf? Get(int shelfId)
        {
            var shelf = Shelves.Find(s => s.Id == shelfId);
            return shelf;
        }

        public string Delete(int Id)
        {
            var shelf = Get(Id);
            if (shelf == null)
                return $"Shelf with{Id} doesn't exists.";
            var name = shelf.Name;
            if (shelf.ShelfBooks.Count > 0)
                return $"Shelf {name} Containt books";
            Shelves.Remove(shelf);
            return $"Shelf '{name}' deleted";
        }

        public List<Shelf> GetAll()
        {
            return Shelves;
        }

        public Book? AddToShelf(BookCreateInShelf request)
        {
            if (request.book == null)
                return null;
            var book = request.book;
            var shelf = Shelves.Find(s => s.Id == request.book.ShelfId);
            if (shelf == null || book == null)
                return null;
            shelf.ShelfBooks.Add(book);
            return book;
        }


        public Book? GetBook(int _id)
        {
            foreach (var item in Shelves)
            {
                foreach (var book in item.ShelfBooks)
                {
                    if (book.Id == _id)
                    {
                        return book;
                    }
                }
            }
            return null;
        }

        public Shelf? MoveTo(MoveToShelfRequest request)
        {
            var book = GetBook(request.BookId);
            var shelf = Shelves.Find(s => s.Id == request.NewShelfId);
            // ესე გადაწყვეტები სწორია????
            if (book == null || shelf == null)
                return null;
            RemoveBook(book.Id);
            book.ShelfId = request.NewShelfId;
            shelf.ShelfBooks.Add(book);
            return shelf;
        }

        public Shelf? RemoveBook(int BookId)
        {
            var book = GetBook(BookId);
            if (book == null)
                return null;
            var shelf = Shelves.Find(s => s.Id == book.ShelfId);
            if (shelf == null)
                return null;
            shelf.ShelfBooks.Remove(book);
            return shelf;
        }
    }
}

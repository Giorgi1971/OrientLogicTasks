using System;
using System.Net;
using BooksInShelf;
using BooksInShelf.Models;
using BooksInShelf.Services;

namespace BooksInShelf.Services
{
    public static class ShelfService
    {
        // task Book 01 - წიგნის დამატება თაროზე (2 overloads).
        public static void AddBookInShelf(int shelfId)
        {
            Console.WriteLine("Enter Book Title");
            string title = Console.ReadLine()!;
            Book book = new Book(title, shelfId);
            Console.WriteLine($"Book {title} created");
        }

        public static void AddBookInShelf(string title, int shelfId)
        {
            Book book = new Book(title, shelfId);
            Console.WriteLine($"Book {title} created");
        }

        // task Book 02 - წიგნის გადაადგილება თაროდან.
        public static void MoveTo(int bookId, int newShelfId)
        {
            var book = Book.books[bookId];
            book.ShelfId = newShelfId;
            Console.WriteLine($"Book {book.Title} Moved in {Shelf.Shelfs[newShelfId].Name} shelf");
        }

        // Task Book 03 - წიგნის წაშლა თაროდან.
        public static void RemoveBook()
        {
            Console.WriteLine("Enter Book's Id For deleting");
            var bookId = int.Parse(Console.ReadLine()!);
            //var book = Book.books.Find(s => s.Id == bookId)!;
            var book = Book.books[bookId - 1];
            Console.WriteLine(book.Title);
            Console.WriteLine(Shelf.Shelfs[book.ShelfId-1].Name);
            Shelf.Shelfs[book.ShelfId-1].shelfBooks.Remove(book);
            Book.books.Remove(book);
            Console.WriteLine($"Book {book.Title} Moved from {Shelf.Shelfs[book.ShelfId - 1].Name} shelf to Trash.");

        }

        // 01 - ერთი თაროს ნახვა და დაბეჭდვა
        public static Shelf GetShelf(int shelf_id)
        {
            var result = Shelf.Shelfs.Find(s => s.Id == shelf_id)!;
            result.DisplayShelf();
            return result;
        }

        //02 - წიგნის თაროს შექმნა
        public static void CreateShelf()
        {
            Console.Write("Enter new Shelf's Name: ");
            var shelfName = Console.ReadLine()!;
            var shelf = new Shelf(shelfName);
        }

        // 03 - თაროს დასახელების შეცვლა
        public static void RenameShelf()
        {
            Console.WriteLine("Enter Shelf Id to Change Name");
            var shelfdId = int.Parse(Console.ReadLine() ?? "");
            var shelf = GetShelf(shelfdId);
            Console.Write("Enter shelf's new Name: ");
            shelf.Name = Console.ReadLine()!;
        }

        public static void BookList()
        {
            var bookList = Book.books;
            foreach (var item in bookList)
            {
                item.Display();
            }
        }

        public static void ChangeShelf()
        {
            // ავარჩიეთ წიგნი
            Console.WriteLine("Enter Book Id to Change Shelf");
            var bookId = int.Parse(Console.ReadLine() ?? "");
            //var book = Book.books.Find(s => s.Id == bookId)!;
            var book = Book.books[bookId-1];
            Console.WriteLine(book.Title);

            // გამოვიღეთ თაროდან
            Console.WriteLine(Shelf.Shelfs[book.ShelfId-1].Name);
            Shelf.Shelfs[book.ShelfId-1].shelfBooks.Remove(book);

            // ჩავდეთ ახალ თაროზე
            Console.WriteLine("Enter new Shelf Id");
            var newShelfId = int.Parse(Console.ReadLine() ?? "");
            Console.WriteLine($"Book {book.Title} Moved in {Shelf.Shelfs[newShelfId-1].Name} shelf");
            book.ShelfId = newShelfId;
            Shelf.Shelfs[newShelfId - 1].shelfBooks.Add(book);
        }

        // 04 - თაროს წაშლა (ყველა წიგნი წაშლილი უნდა იყოს)
        public static void DeleteShelf()
        {
            Console.WriteLine("For Delete Enter Shelf Id");
            var DeleteId = int.Parse(Console.ReadLine() ?? "");

            var shelf = Shelf.Shelfs.Find(s => s.Id == DeleteId);
            if (shelf != null && shelf.shelfBooks.Count <= 0)
            {
                Shelf.Shelfs.Remove(shelf);
                Console.WriteLine($"Shelf {shelf.Name} deleted");
            }
            else
                Console.WriteLine($"Shelf Containt books");
        }

        public static void showShelfs()
        {
            var list = Shelf.Shelfs;
            foreach (var item in list)
            {
                item.DisplayShelf();
            }
        }
    }
}

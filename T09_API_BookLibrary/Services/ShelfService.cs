using System;
using T09_API_BookLibrary.Models;

namespace T09_API_BookLibrary.Services
{
    public class ShelfService
    {
        static List<Shelf> Shelves { get; }
        //private static int _id = 1;

        static ShelfService()
        {
            Shelves = new List<Shelf>();
            //_id = 1;
            var shelf1 = new Shelf("Classics");
            var shelf2 = new Shelf("Lirics");
            var shelf3 = new Shelf("Detectivs");
            Shelves.Add(shelf1);
            Shelves.Add(shelf2);
            Shelves.Add(shelf3);
        }

        //public ShelfService()
        //{
        //}


        // მეთოდები GET_ALL, GET, POST, PUT, DELETE:

        public static List<Shelf> GetAll()
        {
            return Shelves;
        }

        // Get(int shelfId) - ერთი თაროს დაბრუნება
        public static Shelf? Get(int id)
        {
            return Shelves.FirstOrDefault(s => s.Id == id);
        }

        // Create(CreateShelfRequest request) - წიგნების თაროს შექმნა
        public static void Add(Shelf shelf)
        {
            //shelf.Id = _id++;
            Shelves.Add(shelf);
        }

        // Delete(int shelfId) - წიგნების თაროს წაშლა(წაშლა შესაძლებელი უნდა იყოს მხოლოდ იმ შემთხვევაში, თუ არცერთ წიგნს არ შეიცავს)
        public static void Delete(int id)
        {
            var shelf = Get(id);
            if (shelf is null)
                return;
            if (shelf.ShelfBooks.Count == 0)
                Shelves.Remove(shelf);
            else
                return;
        }

        // Rename(RenameShelfRequest reques) -  წიგნების თაროს სახელის შეცვლა
        public static void Update(Shelf shelf)
        {
            var index = Shelves.FindIndex(p => p.Id == shelf.Id);
            Shelves[index] = shelf;
        }
    }
}


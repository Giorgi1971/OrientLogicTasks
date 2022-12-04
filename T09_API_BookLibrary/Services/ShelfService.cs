using System;
using T09_API_BookLibrary.Models;

namespace T09_API_BookLibrary.Services
{
    public class ShelfService
    {
        static List<Shelf> Shelves { get; }
        static int _id { get; set; }

        static ShelfService()
        {
            Shelves = new List<Shelf>();
            _id = 1;
        }


        // მეთოდები GET_ALL, GET, POST, PUT, DELETE:

        public static List<Shelf> GetAll()
        {
            return Shelves;
        }

        public static Shelf? Get(int id)
        {
            return Shelves.FirstOrDefault(s => s.Id == id);
        }


        public static void Add(Shelf shelf)
        {
            shelf.Id = _id++;
            Shelves.Add(shelf);
        }

        public static void Delete(int id)
        {
            var shelf = Get(id);
            if (shelf is null)
                return;
            Shelves.Remove(shelf);
        }

        public static void Update(Shelf shelf)
        {
            var index = Shelves.FindIndex(p => p.Id == shelf.Id);
            Shelves[index] = shelf;
        }
    }
}


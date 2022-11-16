using System;
namespace Task_08_Warehouse
{
    public class Product
    {
        // რა განსხვავებაა name რომ იყოს??? ეს get და set რა აკეთებს, მაინც ხომ ვწვდები.
        // ვალიდაცია აქ რომ იყოს უკეთესი ხომ არ იქნება???
        // private ველები რომ იყოს ესენი და მხოლოდ აქ შეგეძლოს პროდუქტის შექმნა, საწყობის ფუნქციას იძახებდეს.
        public string Name { get; set; }

        public decimal Price { get; set; }

        public uint Quantity { get; set; }

        // public string Category { get; set; }

        public Product(string name, decimal price, uint quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public void DisplayProduct()
        {
            if (Quantity == 0)
                Console.WriteLine($"{Name} - {Quantity} pics, with price {Price}$. Important! Out of stock!!!!RED!!!!");
            else
                Console.WriteLine($"{Name} - {Quantity} pics, with price {Price}$.");

        }
    }
}


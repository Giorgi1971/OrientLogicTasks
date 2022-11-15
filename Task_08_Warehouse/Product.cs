using System;
namespace Task_08_Warehouse
{
    public class Product
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public uint Quantity { get; set; }


        public Product(string name, decimal price, uint quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        string ValidateName(string _name)
        {
            if (_name.Length > 1 && _name.Length < 50) // && name != "unknoun" && !Char.IsDigit(value[0]))
            {
                return _name;
            }
            else
            {
                return "Unknoun";
            }

        }
        //public Product ProductRegistration(string Name, decimal Price, uint Quantity)
        //{

        //    ;
        //}

        public void DisplayProduct()
        {
            Console.WriteLine($"{Name} - {Quantity} pics, with price {Price}$.");
        }
    }
}


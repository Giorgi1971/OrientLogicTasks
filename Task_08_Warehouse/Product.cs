using System;
namespace Task_08_Warehouse
{
    public class Product
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value.Length > 1 && value.Length < 50) //  || !Char.IsDigit(value[0])
                    name = value;
                else
                    Console.WriteLine("NAME NOt Value"); ;
            }
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set
            {
                if (value >= 0)
                {
                    Console.WriteLine("Number is Positive!");
                    price = value;
                }
                else
                {
                    Console.WriteLine("Number is NOOOT Positive!");
                }
            }
        }

        public uint Quantity { get; set; }


        public Product(string name, decimal tt, uint quantity)
        {
            Name = name;
            Price = tt;
            Quantity = quantity;
        }

        //public Product ProductRegistration(string Name, decimal Price, uint Quantity)
        //{

        //    ;
        //}

        public void DisplayProduct()
        {
            Console.WriteLine($"{name} - {Quantity} pics, with price {price}$.");
        }
    }
}


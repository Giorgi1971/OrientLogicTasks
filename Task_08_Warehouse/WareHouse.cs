using System;
using System.IO;

namespace Task_08_Warehouse

{
    public class WareHouse
    {
        public static string NameWareHose = "WareHouse_001";
        public List<Product> Products { get; set; }

        public WareHouse()
        {
            Products = new List<Product>();
        }

        public void DisplayAllProducts()
        {
            if (Products == null)
                Console.WriteLine("No items in WareHouse!");
            else
            {
                Console.WriteLine("\n\tProduct List: ");
                foreach (var item in Products)
                {
                    item.DisplayProduct();
                }
            }
        }

        string getName()
        {
            while (true)
            {
                Console.WriteLine("Enter Product Name: ");
                var _name = Console.ReadLine();
                if(_name.Length > 1 && _name.Length < 50 && !Char.IsDigit(_name[0]))
                {
                    return _name;
                }
                else
                {
                    System.Console.WriteLine( "Enter Valid Name!");
                    continue;
                }
            }
        }

        // Parse უკეთესია, TryParse თუ Convert ასეთ შემთხვევბში და რატომ???
        decimal getPrice()
        {
            while(true)
                {
                Console.WriteLine("Enter Product Price: ");
                string? str = Console.ReadLine();
                try
                {
                    decimal price = Decimal.Parse(str);
                    Console.WriteLine("'{0}' converted to {1}.", str, price);
                    if(price <= 0)
                    {
                        throw new FormatException();
                    }
                    return price;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to parse '{0}'.", str);
                    continue;
                }
            }
        }

        uint getQuantity()
        {
            while (true)
            {
                uint result;
                Console.WriteLine("Enter quantity");
                string value = Console.ReadLine();
                try
                {
                    result = Convert.ToUInt32(value);
                    return result;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("The {0} value '{1}' is outside the range of the UInt32 type.",
                                      value.GetType().Name, value);
                    continue;
                }
                catch (FormatException)
                {
                    Console.WriteLine("The {0} value '{1}' is not in a recognizable format.",
                                      value.GetType().Name, value);
                    continue;
                }
            }
        }

        public void RegisterProduct()
        {
            var name = getName();
            if (Products.Exists(x => x.Name == name))
            {
                System.Console.WriteLine( "Product with this name already exists!");
                return;
            }
            var price = getPrice();
            var quantity = getQuantity();
            try
            {
                Product product = new Product(name, price, quantity);
                Products.Add(product);
                Console.WriteLine($"{name} added in Products list");
            }
            catch
            {
                Console.WriteLine("Object not created");
            }
        }

        public void RemoveProducts()
        {
            var product = GetProduct();
            Products.Remove(product);
        }

        public void UpdateProducts()
        {
            var product = GetProduct();
            var price = getPrice();
            var quantity = getQuantity();
            product.Price = price;
            product.Quantity = quantity;
        }

        Product GetProduct()
        {
            var name = getName();
            Product product = Products.Find(x => x.Name == name);
            return product;
        }
    }
}


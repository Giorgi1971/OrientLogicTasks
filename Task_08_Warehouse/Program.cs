// See https://aka.ms/new-console-template for more information
using System.Linq;
using Task_08_Warehouse;


Console.WriteLine("Hello, World!");

var ll = 3.3m;


Product product1 = new Product("Bread", (decimal)2.0, 300);
product1.DisplayProduct();
Product product2 = new Product("Cup", (decimal)2.7, 300);
product2.DisplayProduct();
Product product3 = new Product("Butter", (decimal)-2.0m, 300);
product3.DisplayProduct();
Product product4 = new Product("Cherry", 3.9m, 900);
product4.DisplayProduct();

WareHouse wareHouse_001 = new WareHouse();

Console.WriteLine(  wareHouse_001.NameWareHose);

Category cat1 = new Category();
var catList = cat1.categories;

foreach (var item in catList)
{
    Console.WriteLine(item);
}
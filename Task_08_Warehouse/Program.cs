// See https://aka.ms/new-console-template for more information
using System.Linq;
using Task_08_Warehouse;
using System;

var wh1 = new WareHouse();

var endWork = false;

Product product1 = new Product("bread", 3.5m, 400);
Product product2 = new Product("Cheese", 12.5m, 200);

wh1.Products.Add(product1);
wh1.Products.Add(product2);

Console.WriteLine($"Hello in warehouse \"{WareHouse.NameWareHose}\".");
Console.WriteLine("We are Begin working.\n");


void PrintMenu()
{
    Console.WriteLine("\n1. Create Product in warehouse;");
    Console.WriteLine("2. Update Product in warehouse;");
    Console.WriteLine("3. Remove Product in warehouse;");
    Console.WriteLine("4. Show all Products in warehouse;");
    Console.WriteLine("5. Exit.");
    Console.Write("\nChoose one of from menu: ");
}


while (!endWork)
{
    PrintMenu();
    byte begin = 7;
    try
    {
        begin = byte.Parse(Console.ReadLine()); ;
    }
    catch (FormatException e)
    {
        Console.WriteLine("\n"+e.Message);
        continue;
    }
    if (begin == 2)
    {
        wh1.UpdateProducts();
    }
    else if (begin == 1)
    {
        wh1.CreateProduct();
    }
    else if (begin == 4)
    {
        wh1.DisplayAllProducts();
    }
    else if (begin == 3)
    {
        wh1.RemoveProducts();
    }
    else if(begin == 5)
    {
        System.Environment.Exit(0);
    }
    else
    {
        Console.WriteLine("Please enter from 1 to 5!");
    }
}
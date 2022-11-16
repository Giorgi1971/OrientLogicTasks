// See https://aka.ms/new-console-template for more information
using System.Linq;
using Task_08_Warehouse;
using System;

var wh1 = new WareHouse();

var endWork = false;

// ეს ქვედა ხაზები უბრალოდ ვქმნი პროდუქტებს, შეიძლება არც გვქონდეს.
Category.Desc = "Mood";
var product1 = new Product("bread", "Food", 3.5m, 400);
var product2 = new Product("Cheese", "Food", 12.5m, 200);
product1.Quantity = 1000;
wh1.Products.Add(product1);
wh1.Products.Add(product2);


Console.WriteLine("We are Begin working.");


static void PrintMenu()
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
    // ამის ინიციალიზება აუცილებელია, სხვანაირად ხომ არ უნდა ვაკეთებდე?
    ushort begin;
    try
    {
        begin = byte.Parse(Console.ReadLine() ?? ""); ;
    }
    catch (FormatException e)
    {
        Console.WriteLine("\n"+e.Message);
        continue;
    }
    catch
    {
        Console.WriteLine("Not a valid Number");
        continue;
    }
    if (begin == 2)
    {
        wh1.UpdateProducts();
    }
    else if (begin == 1)
    {
        wh1.RegisterProduct();
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

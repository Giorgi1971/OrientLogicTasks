// See https://aka.ms/new-console-template for more information
using System.Linq;
using Task_08_Warehouse;
using System;



var endWork = false;

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

var wh1 = new WareHouse();


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

    }
    else if (begin == 1)
    {

    }
    else if (begin == 4)
    {
        wh1.DisplayAllProducts();
    }
    else if (begin == 3)
    {

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
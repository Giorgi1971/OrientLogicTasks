/* შეკვეთების მიცემა (სტილი მაკ-დონალდსი)
 * გამოდის მენიუ სადაც შეგვიძლია შევუკვეთოთ სასმელები (როგორც მზა ასევე ჩვენით აწყობილი, ლაღიძის წყლები)
 * და ბურგერები. გამოყენებულია design pattern - Builder (დირექტორის გარეშე) 
 * ბურგერი დასამთავრებელია. შეკვეთისას ფასებსაც რომ ითვლიდეს იქნებ მერე მოვახერხო....
 * 
 */


using System;
using T8_Builder_DesignPatterns.Builders;
using T8_Builder_DesignPatterns.Models;

namespace T8_Builder_DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            var dd1 = new ReadyDrink() { Name = "Coca-Cola", Price = 5, Cup = { "Cola", "Suger", "Water" } };
            var dd2 = new ReadyDrink() { Name = "Pepsi", Price = 4, Cup = { "Pepsi", "Suger", "Water" } };


            Console.WriteLine("Hello World!");

            var ordering = true;
            var choiceDrink = true;

            while (ordering)
            {
                Console.WriteLine("\t\t\tWelcome - Begin Order:\n1 Drink\t\t2 Burger\t3 Orders\t9 Cancel\n");
                Menu();
            }
       

            void Menu()
            {
                Console.Write("Enter Choise: ");
                var choiseMenu = Console.ReadLine();
                switch (choiseMenu)
                {
                    case "1":
                        DrinkMenu();
                        break;
                    case "2":
                        BurgerMenu();
                        break;
                    case "3":
                        ShowOrder();
                        break;
                    case "9":
                        ordering = false;
                        break;
                    default:
                        Console.WriteLine("Choose Correctly");
                        break;
                }
            }


            void DrinkMenu()
            {
                choiceDrink = true;
                while (choiceDrink)
                {
                    Console.WriteLine("1 ReadyDrink\t2 CustomDrink\t3 backMenu\t9 Cancel");
                    var orderDrink = Console.ReadLine();
                    if (orderDrink == "1")
                    {
                        ReadyDrink();
                    }
                    else if (orderDrink == "2")
                    {
                        CustomDrink();
                    }
                    else if (orderDrink == "3")
                    {
                        choiceDrink = false;
                    }
                    else if (orderDrink == "9")
                    {
                        ordering = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Choose Correctly");
                    }
                }
            }


            void ReadyDrink()
            {
                var rdord = true;
                while (rdord)
                {
                    Console.WriteLine("1. Coca-Cola; \t\t 2. Pepsi\t 3. Back");
                    var switch_on = Console.ReadLine();
                    switch (switch_on)
                    {
                        case "1":
                            Order.Orders.Add(dd1);
                            Console.WriteLine("Coca Cola Added in Orders");
                            rdord = false;
                            break;
                        case "2":
                            Order.Orders.Add(dd1);
                            Console.WriteLine("Pepsi Added in Orders");
                            rdord = false;
                            break;
                        case "3":
                            rdord = false;
                            break;
                        default:
                            Console.WriteLine("Choose from list, enter Number.");
                            break;
                    }
                }
            }

                    void CustomDrink()
            {
                var drinkBuilder = new DrinkBuilder();
                drinkBuilder.ResetDrink();
                drinkBuilder.WithWather();
                var dord = true;
                while(dord)
                {
                    Console.WriteLine
                        (
                        "1 Suger;   2 Nagebi;   3 Chocolate;   4 Kotsakhuri;   5 Ready;   9 Cancel."
                        );
                    var switch_on = Console.ReadLine();
                    switch (switch_on)
                    {
                        case "1":
                            drinkBuilder.WithSpoonSuger();
                            break;
                        case "2":
                            drinkBuilder.WithNagebi();
                            break;
                        case "3":
                            drinkBuilder.WithChocolate();
                            break;
                        case "4":
                            drinkBuilder.WithKhotsakhuri();
                            break;
                        case "5":
                            var objDrink = drinkBuilder.Build();
                            Order.Orders.Add(objDrink);
                            dord = false;
                            break;
                        case "8":
                            dord = false;
                            break;
                        case "9":
                            ordering = false;
                            choiceDrink = false;
                            dord = false;
                            break;
                    }
                }
            }


            void BurgerMenu()
            {
                var burgerBuilder = new BurgerBuilder();
                burgerBuilder.ResetBurger();
                burgerBuilder.WithBeef();
                burgerBuilder.WithCheese();
                var prod1 = burgerBuilder.Build();
                foreach (var item in prod1.Ingredients)
                {
                    Console.WriteLine(item);
                }
            }


            void ShowOrder()
            {
                var allOrder = Order.Orders;

                if(allOrder.Count < 1)
                    Console.WriteLine("You havn't orders yet");
                else
                {
                    foreach (var item in allOrder)
                    {
                        Console.WriteLine(item.ToString());
                    }
                }
            }
        }
    }
}

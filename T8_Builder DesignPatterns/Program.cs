using System;
using T8_Builder_DesignPatterns.Builders;
using T8_Builder_DesignPatterns.Models;

namespace T8_Builder_DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var ordering = true;
            WelcomeMenu();

            void WelcomeMenu()
            {
                while(ordering)
                {
                    DisplayMenu();
                    ChoiseMenu();
                    // ordering = false;
                }
            }

            void CreateDrink()
            {
                while (true)
                {
                    Console.WriteLine("1. Ready Drinks;\t\t 2. Custom Drinks.\t\t5. Cancel Order");
                    var choiseOrd = Console.ReadLine();
                    if (choiseOrd == "1")
                    {
                        ReadyDrink();
                    }
                    else if (choiseOrd == "2")
                    {
                        CustomDrink();
                    }
                    else if (choiseOrd == "5")
                    {
                        ordering = false;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            void CustomDrink()
            {
                var drinkBuilder = new DrinkBuilder();
                drinkBuilder.ResetDrink();
                drinkBuilder.WithWather();

                var dd = drinkBuilder.Build();
                Console.WriteLine(dd);
            }

            void ReadyDrink()
            {
                Console.WriteLine("Coca-Cola");
                Console.WriteLine("Pepsi");
            }

            void CreateBurger()
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

            void ResetAll()
            {
                ordering = false;
                // var drinkBuilder = new DrinkBuilder();
            }

            void OrderMenu()
            {
                // var drinkBuilder = new DrinkBuilder();
            }

            void ChoiseMenu()
            {
                Console.WriteLine("Your Choise: ");
                var choiseMenu = Console.ReadLine();
                switch (choiseMenu)
                {
                    case "1":
                        CreateDrink();
                        break;
                    case "2":
                        CreateBurger();
                        break;
                    case "3":
                        ResetAll();
                        break;
                    case "4":
                        OrderMenu();
                        break;
                    default:
                        throw new ArithmeticException("Access denied - You must be at least 18 years old.");
                }
                Console.WriteLine(choiseMenu);
            }


            static void DisplayMenu()
            {
                Console.WriteLine("\t\t\tWelcome");
                Console.Write("1. Drink");
                Console.Write("\t2. Burger");
                Console.Write("\t3. CancelMenu");
                Console.Write("\t4. Order\n");
            }
        }
    }
}

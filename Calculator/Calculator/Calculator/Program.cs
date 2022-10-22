using System;

namespace MyApplication
{
    class Program
    {

        static int[] getData()
        {

            while (true)
            {
                try
                {
                    Console.Write("Enter First number: ");
                    int x = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Second number: ");
                    int y = Convert.ToInt32(Console.ReadLine());
                    int[] treeValue = { x, y };
                    return treeValue;
                }
                catch (Exception)
                {
                    Console.WriteLine("text is not number.");
                }
                
            }

        }


        static string Operator()
        {
            string[] operators = { "+", "-", "*", "/" };
            Console.WriteLine(operators);
            while (true)
            {
                Console.WriteLine("Enter Operator: + - * / ");
                string? y = Console.ReadLine();
                if (y == null)
                {
                    Console.WriteLine(y);
                    continue;
                }
                if (Array.Exists(operators, Predicate y)
                {
                    return y;
                }

            }
        }

        static void Main(string[] args)
        {
            int[] nums = getData();
            int a = nums[0];
            int b = nums[1];
            string ope = Operator();
            decimal result = 0;
            switch (ope)
            {
                case "+":
                    result = a + b;
                    break;
                case "-":
                    result = a - b;
                    break;
                case "/":
                    try
                    {
                        result = a / b;
                    }
                    catch (DivideByZeroException)
                    {
                        Console.WriteLine("Division of {0} by zero.", a);
                    }
                    break;
                case "*":
                    result = a * b;
                    break;
            }
            if (b != 0 || ope != "/")
            {
                Console.WriteLine("result: " + result);
            }
        }
    }
}
// Made without class and functions

using System;
using System.Data;


int number1;
int number2;
char oper;
char[] operators = { '+', '-', '*', '/' };

while (true)
{
    Console.Write("Enter first number: ");
    string inputString = Console.ReadLine();

    if (Int32.TryParse(inputString, out number1))
    {
        break;
    }
    else
    {
        Console.WriteLine($"{inputString} is not a number\n");
    }
}

while (true)
{
    Console.Write("Enter second number: ");
    string inputString = Console.ReadLine();

    if (Int32.TryParse(inputString, out number2))
    {
        break;
    }
    else
    {
        Console.WriteLine($"{inputString} is not a number\n");
    }
}

while (true)
{
    Console.Write("Enter Math operation (+ - * / ): ");
    string opera = Console.ReadLine();

    if (Char.TryParse(opera, out oper))
    {
        if(operators.Contains(oper))
        {
            break;
        }
    }
    else
    {
        Console.WriteLine($"{opera} is not in \"+ - * / \"\n");
    }

}

if ((oper == '/') && (number2 == 0))
{
    Console.WriteLine("Cannot divide by zero.\n");
}
else
{
    double result = Convert.ToDouble(
        new DataTable().Compute($"{number1} {oper} {number2}", null)
        );
    Console.WriteLine($"{number1} {oper} {number2} = {result}");
}

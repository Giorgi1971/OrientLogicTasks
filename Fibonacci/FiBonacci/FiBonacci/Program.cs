using System;


Console.WriteLine("Hello World!");
int n = 0;
while (true)
{
    Console.Write("Enter number for calculate Fibonachi number: ");
    string inputString = Console.ReadLine();
    if (Int32.TryParse(inputString, out n))
    {
        break;
    }
    else
    {
        Console.WriteLine($"{inputString} is not a number\n");
    }
}

if (n == 1)
{
    Console.Write(0);
}
else if (n == 2)
{
    Console.Write(1);
}
else
{
    int a = 0;
    int b = 1;
    int c = 0;
    for (int i = 2; i < n; i++)
    {
        c = a + b;
        a = b;
        b = c;
    }
    Console.Write(c);
}

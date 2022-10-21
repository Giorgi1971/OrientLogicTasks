using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            int n = 0;
            while (true)
            {
                Console.WriteLine("Enter ineger: ");
                n = Convert.ToInt32(Console.ReadLine());
            }
            int result = CalculateFibonacci(n);
            Console.WriteLine(result);
        }

        long CalculateFibonacci(int n)
        {
            if(n == 1)
            {
                return 0;
            }
            else if (n == 2)
            {
                return 1;
            }
            else
            {
                int a = 0;
                int b = 1;
                int c = 0;
                for (int i = 0; i < n; i++) 
                {
                    c = a + b;
                    a = b;
                    b = c;
                }
                return c;
            }
        }
    }
}
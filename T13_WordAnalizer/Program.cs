// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Thread mainThread = Thread.CurrentThread;
mainThread.Name = "Main Thread";
Console.WriteLine(mainThread.Name);







Console.ReadKey();
